(() => {
    const Cliente = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },

        inicializarTabla() {
            this.tabla = $('#tblCliente').DataTable({
                ajax: {
                    url: '/Cliente/ObtenerClientes',
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'id' },
                    { data: 'nombre' },
                    { data: 'correo' },
                    { data: 'telefono' },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-sm btn-primary btn-editar" data-id="${row.id}">
                                    <i class="bi bi-pencil"></i> Editar
                                </button>
                                <button class="btn btn-sm btn-danger btn-eliminar" data-id="${row.id}">
                                    <i class="bi bi-trash"></i> Eliminar
                                </button>`;
                        }
                    }
                ],
                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }
            });
        },

        registrarEventos() {
            // Crear
            $('#btnGuardarCliente').on('click', () => this.guardarCliente());

            // Editar
            $('#tblCliente').on('click', '.btn-editar', (e) => {
                const id = $(e.currentTarget).data('id');
                this.cargarDatosCliente(id);
            });

            $('#btnActualizarCliente').on('click', () => this.editarCliente());

            // Eliminar
            $('#tblCliente').on('click', '.btn-eliminar', (e) => {
                const id = $(e.currentTarget).data('id');
                this.eliminarCliente(id);
            });
        },

        guardarCliente() {
            let form = $('#formCrearCliente');
            if (!form[0].checkValidity()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: (response) => {
                    if (response.esCorrecto) {
                        $('#modalCrearCliente').modal('hide');
                        form[0].reset();
                        this.tabla.ajax.reload(null, false);

                        Swal.fire({
                            title: 'Éxito',
                            text: response.mensaje,
                            icon: 'success'
                        });
                    }
                }
            });
        },

        cargarDatosCliente(id) {
            $.get(`/Cliente/ObtenerClientePorId?id=${id}`, (response) => {
                if (response.esCorrecto) {
                    let data = response.data;
                    $('#ClienteId').val(data.id);
                    $('#Nombre').val(data.nombre);
                    $('#Correo').val(data.correo);
                    $('#Telefono').val(data.telefono);

                    $('#modalEditarCliente').modal('show');
                }
            });
        },

        editarCliente() {
            let form = $('#formEditarCliente');
            if (!form[0].checkValidity()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: (response) => {
                    if (response.esCorrecto) {
                        $('#modalEditarCliente').modal('hide');
                        form[0].reset();
                        this.tabla.ajax.reload(null, false);

                        Swal.fire({
                            title: 'Éxito',
                            text: response.mensaje,
                            icon: 'success'
                        });
                    } else {
                        Swal.fire({
                            title: 'Error',
                            text: response.mensaje,
                            icon: 'warning'
                        });
                    }
                }
            });
        },

        eliminarCliente(id) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: '¡No podrás revertir esta acción!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Cliente/EliminarCliente',
                        type: 'POST',
                        data: { id: id },
                        success: (response) => {
                            if (response.esCorrecto) {
                                this.tabla.ajax.reload(null, false);
                                Swal.fire({
                                    title: 'Éxito',
                                    text: response.mensaje,
                                    icon: 'success'
                                });
                            }
                        }
                    });
                }
            });
        }
    };

    $(document).ready(() => Cliente.init());
})();