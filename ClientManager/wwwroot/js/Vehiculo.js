let tablaVehiculos;

$(document).ready(function () {

    tablaVehiculos = $("#tablaVehiculos").DataTable({
        destroy: true,
        responsive: true,
        processing: true,
        ajax: {
            url: "/Vehiculo/ObtenerVehiculos",
            type: "GET",
            dataSrc: function (json) {
                // 👇 Déjalo temporalmente para ver qué está llegando
                console.log("JSON Vehiculos:", json);

                // Si por alguna razón "data" no viene, devolvemos arreglo vacío
                return (json && json.data) ? json.data : [];
            }
        },
        columns: [
            { data: "placa" },
            { data: "marca" },
            { data: "modelo" },
            { data: "clienteId" },
            {
                data: "id",
                orderable: false,
                searchable: false,
                render: function (data) {
                    return `
                        <button class="btn btn-sm btn-warning me-1" onclick="editar(${data})" title="Editar">
                            <i class="bi bi-pencil"></i>
                        </button>
                        <button class="btn btn-sm btn-danger" onclick="eliminar(${data})" title="Eliminar">
                            <i class="bi bi-trash"></i>
                        </button>
                    `;
                }
            }
        ],
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json"
        }
    });

});

// =============================
// Helpers
// =============================
function toIntOrZero(value) {
    const n = parseInt(value, 10);
    return Number.isFinite(n) ? n : 0;
}

// =============================
// ABRIR MODAL
// =============================
function abrirModal() {
    limpiarFormulario();
    $("#modalVehiculo").modal("show");
}

// =============================
// LIMPIAR FORMULARIO
// =============================
function limpiarFormulario() {
    $("#Id").val(0);
    $("#Placa").val("");
    $("#Marca").val("");
    $("#Modelo").val("");
    $("#ClienteId").val("");
}

// =============================
// GUARDAR (AGREGAR / ACTUALIZAR)
// =============================
function guardar() {

    const id = toIntOrZero($("#Id").val());
    const clienteId = toIntOrZero($("#ClienteId").val());

    const data = {
        Id: id,
        Placa: ($("#Placa").val() || "").trim(),
        Marca: ($("#Marca").val() || "").trim(),
        Modelo: ($("#Modelo").val() || "").trim(),
        ClienteId: clienteId
    };

    // Validación rápida del lado UI (además del ModelState del backend)
    if (!data.Placa || !data.Marca || !data.Modelo || data.ClienteId <= 0) {
        Swal.fire({
            icon: "error",
            title: "Error",
            text: "Por favor complete Placa, Marca, Modelo y ClienteId (mayor a 0)."
        });
        return;
    }

    const url = data.Id === 0
        ? "/Vehiculo/AgregarVehiculo"
        : "/Vehiculo/ActualizarVehiculo";

    $.post(url, data)
        .done(function (response) {

            if (response && response.esCorrecto) {

                $("#modalVehiculo").modal("hide");

                // Recarga sin resetear paginación
                tablaVehiculos.ajax.reload(null, false);

                Swal.fire({
                    icon: "success",
                    title: "Éxito",
                    text: "Operación realizada correctamente"
                });

            } else {
                Swal.fire({
                    icon: "error",
                    title: "Error",
                    text: (response && response.mensaje) ? response.mensaje : "No se pudo completar la operación."
                });
            }

        })
        .fail(function (xhr) {
            console.error("POST fallo:", xhr);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: "Ocurrió un error inesperado al guardar."
            });
        });
}

// =============================
// EDITAR
// =============================
function editar(id) {

    $.get("/Vehiculo/ObtenerVehiculoPorId", { id: id })
        .done(function (response) {

            if (response && response.esCorrecto) {

                const v = response.data;

                $("#Id").val(v.id);
                $("#Placa").val(v.placa);
                $("#Marca").val(v.marca);
                $("#Modelo").val(v.modelo);
                $("#ClienteId").val(v.clienteId);

                $("#modalVehiculo").modal("show");

            } else {
                Swal.fire("Error", (response && response.mensaje) ? response.mensaje : "No se pudo obtener el vehículo.", "error");
            }

        })
        .fail(function (xhr) {
            console.error("GET editar fallo:", xhr);
            Swal.fire("Error", "No se pudo obtener el vehículo (falló la petición).", "error");
        });
}

// =============================
// ELIMINAR
// =============================
function eliminar(id) {

    Swal.fire({
        title: "¿Está seguro?",
        text: "No podrá revertir esta acción",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {

        if (!result.isConfirmed) return;

        $.post("/Vehiculo/EliminarVehiculo", { id: id })
            .done(function (response) {

                if (response && response.esCorrecto) {

                    tablaVehiculos.ajax.reload(null, false);
                    Swal.fire("Eliminado", "Vehículo eliminado correctamente", "success");

                } else {
                    Swal.fire("Error", (response && response.mensaje) ? response.mensaje : "No se pudo eliminar.", "error");
                }

            })
            .fail(function (xhr) {
                console.error("POST eliminar fallo:", xhr);
                Swal.fire("Error", "No se pudo eliminar (falló la petición).", "error");
            });
    });
}