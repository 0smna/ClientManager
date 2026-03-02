let tablaCitas;

$(document).ready(function () {

    tablaCitas = $("#tablaCitas").DataTable({
        destroy: true,
        responsive: true,
        ajax: {
            url: "/Cita/ObtenerCitas",
            type: "GET",
            dataSrc: "data"
        },
        columns: [
            { data: "placa" },
            { data: "fechaCita" },
            {
                data: "estado",
                render: function (data) {
                    if (data === 0) return "Ingresada";
                    if (data === 1) return "Cancelada";
                    if (data === 2) return "Concluida";
                    return "";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return `
                        <button class="btn btn-sm btn-success me-1"
                            onclick="cambiarEstado(${data}, 2)">
                            Concluir
                        </button>
                        <button class="btn btn-sm btn-danger"
                            onclick="cambiarEstado(${data}, 1)">
                            Cancelar
                        </button>
                    `;
                }
            }
        ]
    });

});

// =====================================
// CARGAR VEHICULOS EN DROPDOWN
// =====================================
function cargarVehiculos() {

    $.get("/Cita/ObtenerVehiculos", function (response) {

        const select = $("#VehiculoId");
        select.empty();

        response.data.forEach(v => {
            select.append(`<option value="${v.id}">${v.placa}</option>`);
        });

    });
}

// =====================================
// ABRIR MODAL
// =====================================
function abrirModal() {

    cargarVehiculos();

    $("#ClienteId").val("");
    $("#FechaCita").val("");

    $("#modalCita").modal("show");
}

// =====================================
// GUARDAR CITA
// =====================================
function guardar() {

    const data = {
        ClienteId: parseInt($("#ClienteId").val()),
        VehiculoId: parseInt($("#VehiculoId").val()),
        FechaCita: $("#FechaCita").val()
    };

    $.post("/Cita/AgregarCita", data, function (response) {

        if (response.esCorrecto) {

            $("#modalCita").modal("hide");
            tablaCitas.ajax.reload(null, false);

            Swal.fire("Éxito", "Cita registrada correctamente", "success");

        } else {

            Swal.fire("Error", response.mensaje, "error");

        }

    });
}

// =====================================
// CAMBIAR ESTADO
// =====================================
function cambiarEstado(id, estado) {

    $.post("/Cita/CambiarEstado", { id: id, estado: estado }, function (response) {

        if (response.esCorrecto !== false) {

            tablaCitas.ajax.reload(null, false);
            Swal.fire("Actualizado", response.mensaje, "success");

        } else {

            Swal.fire("Error", response.mensaje, "error");

        }

    });
}