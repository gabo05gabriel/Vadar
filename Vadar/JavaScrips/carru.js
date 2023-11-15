document.addEventListener("DOMContentLoaded", function () {
    const carrusel = document.querySelector(".carrusel");
    const imagenes = document.querySelectorAll(".imagen");
    const botonAnterior = document.getElementById("anterior");
    const botonSiguiente = document.getElementById("siguiente");
    let indice = 0;

    function mostrarImagen(indice) {
        carrusel.style.transform = `translateX(-${indice * 100}%)`;
    }

    botonAnterior.addEventListener("click", function () {
        if (indice > 0) {
            indice--;
            mostrarImagen(indice);
        }
    });

    botonSiguiente.addEventListener("click", function () {
        if (indice < imagenes.length - 1) {
            indice++;
            mostrarImagen(indice);
        }
    });
});
