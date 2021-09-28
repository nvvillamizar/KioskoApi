"use strict";


document.getElementById("clear").addEventListener("click", limpiarBuscador);
document.getElementById("buscarInformacion").addEventListener("click", buscarInformacion);

var exportarReportes = document.getElementsByClassName('exportarReporte');
var numeroReportes = exportarReportes.length;

for (var i = 0; i < numeroReportes; i++) {
    exportarReportes[i].addEventListener('click', descargarNuevoReporte);
}

window.onload = function () {
    console.log("Nuevos cambios");
    buscarInformacion();
}

function limpiarBuscador() {
    document.getElementById("textName").value = "";
    buscarInformacion();
}

function eEspecialidad(nombreEspecialidad, idEspecialidad) {
    console.log(idEspecialidad, '', nombreEspecialidad);

    mostrarModal(`Atención`, `Se eliminará la especialidad ${nombreEspecialidad}, ¿Desea continuar?`).then(result => {
        if (result.isConfirmed) {
            $.get(`Especialidad/Eliminar/?idEspecialidad=${idEspecialidad}`, function (resp) {
                if (resp = 1) buscarInformacion();
            })
        }
    });
}

function descargarNuevoReporte() {

    const tipoArchivoSeleccionado = this.dataset.type;
    if (tipoArchivoSeleccionado == "print") {
        imprimirReporte();
        return;
    }
    document.getElementById("tipoReporte").value = tipoArchivoSeleccionado;

    document.getElementById("formularioReporte").submit();
}

function imprimirReporte() {
    const contenidoCheck = document.getElementById("check").outerHTML;
    let table = "<h1>Reporte Especialidades</h1>";
    table += document.getElementById("table").outerHTML;
    table = table.replace(contenidoCheck, "");
    const pagina = window.document.body;
    const ventana = window.open();
    ventana.document.write(table);
    ventana.print();
    ventana.close();
    window.document.body = pagina;
}

function buscarInformacion() {

    let nombre = (document.getElementById("textName").value == "") ? null : document.getElementById("textName").value;
    let tbody = document.getElementById("datos");
    let contenido = "";

    $.get(
        `Especialidad/BuscarEspecialidad/?nombreEspecialidad=${nombre}`,
        function (data) {
            for (let i = 0; i < data.length; i++) {
                contenido += `<tr>
                    <td>${data[i].idEspecialidad}</td>
                    <td>${data[i].nombre}</td>
                    <td>${data[i].descripcion}</td>
                    <td>
                        <a class="btn btn-info" asp-controller="Especialidad" asp-action="Editar" asp-route-idEspecialidad="${data[i].idEspecialidad}">Editar</a>
                        <a class="btn btn-danger" onclick="eEspecialidad('${data[i].nombre}',${data[i].idEspecialidad})">Eliminar</a>
                    </td>
                 </tr>`;
            }

            tbody.innerHTML = contenido;
        }
    );
}
