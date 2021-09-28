"use strict";

function mostrarModal(titulo = "¿Deseas guardar los cambios?", texto = "Al pulsar SI, se guardará la información en la base de datos.", icono = "warning",botonConfirmar = "Si") {
    return Swal.fire({
        title: titulo,
        icon: icono,
        html: texto,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: botonConfirmar
    })
}