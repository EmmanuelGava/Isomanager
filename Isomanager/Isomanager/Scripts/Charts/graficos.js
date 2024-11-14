// graficos.js

// Función para obtener datos de la API
async function obtenerDatos() {
    try {
        const response = await fetch('https://localhost:44353/api/Usuarios', {
            headers: {
                'Accept': 'application/json' // Solicitar JSON
            }
        });
        if (!response.ok) {
            throw new Error('Error al obtener los datos de la API');
        }
        const data = await response.json();
        console.log('Datos obtenidos de la API:', data); // Log de los datos
        return data;
    } catch (error) {
        console.error('Error:', error);
        return []; // Retorna un array vacío en caso de error
    }
}

// Función para crear el gráfico de líneas
async function crearGrafico() {
    const datos = await obtenerDatos();

    // Calcular el promedio de todos los usuarios
    const promediosPorMes = {};

    // Recorrer cada usuario y sus desempeños
    datos.forEach(usuario => {
        usuario.Desempenos.forEach(desempeno => {
            if (!promediosPorMes[desempeno.Mes]) {
                promediosPorMes[desempeno.Mes] = { total: 0, count: 0 };
            }
            promediosPorMes[desempeno.Mes].total += desempeno.Promedio;
            promediosPorMes[desempeno.Mes].count += 1;
        });
    });

    // Crear etiquetas y promedios finales
    const etiquetas = Object.keys(promediosPorMes);
    const promedios = etiquetas.map(mes => {
        // Si solo hay un usuario, el promedio es el total
        if (promediosPorMes[mes].count === 1) {
            return promediosPorMes[mes].total; // Solo un usuario, así que el promedio es el total
        }
        return promediosPorMes[mes].total / promediosPorMes[mes].count; // Promedio normal
    });

    console.log('Etiquetas:', etiquetas); // Log de etiquetas
    console.log('Promedios:', promedios); // Log de promedios

    const ctx = document.getElementById('ChartPromedioUsuario').getContext('2d');
    const miGrafico = new Chart(ctx, {
        type: 'line', // Cambia a 'line' para un gráfico de líneas
        data: {
            labels: etiquetas,
            datasets: [{
                label: 'Promedio de Desempeño',
                data: promedios,
                fill: false,
                borderColor: 'rgba(75, 192, 192, 1)',
                tension: 0.1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            plugins: {
                legend: {
                    position: 'top',
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            return tooltipItem.label + ': ' + tooltipItem.raw;
                        }
                    }
                }
            }
        }
    });
}

// Llamar a la función para crear el gráfico cuando el DOM esté listo
document.addEventListener('DOMContentLoaded', function () {
    crearGrafico(); // Cargar el gráfico para todos los usuarios al inicio
});


// Grafico Formacion

// Función para obtener datos de la API
// graficos.js

// Función para obtener datos de formación de un usuario
async function obtenerDatosFormacion(usuarioId) {
    try {
        const response = await fetch(`https://localhost:44353/api/Usuarios/${usuarioId}/formacion`);
        if (!response.ok) {
            throw new Error('Error al obtener los datos de formación');
        }
        const data = await response.json();
        console.log('Datos de formación obtenidos:', data); // Log de los datos
        return data;
    } catch (error) {
        console.error('Error:', error);
        return []; // Retorna un array vacío en caso de error
    }
}

// Función para crear el gráfico de horas de formación por área
async function crearGraficoFormacionPorArea(usuarioId) {
    const datos = await obtenerDatosFormacion(usuarioId);

    // Inicializar horas por área
    const horasPorArea = {
        Calidad: 0,
        Seguridad: 0,
        Ambiental: 0,
        Procesos: 0,
        Liderazgo: 0
    };

    // Sumar las horas de formación
    datos.forEach(formacion => {
        if (formacion.Area in horasPorArea) {
            horasPorArea[formacion.Area] += formacion.Horas || 0;
        }
    });

    const etiquetas = Object.keys(horasPorArea);
    const horas = Object.values(horasPorArea);

    console.log('Etiquetas:', etiquetas); // Log de etiquetas
    console.log('Horas:', horas); // Log de horas

    // Crear el gráfico
    var ctx2 = document.getElementById('ChartFormacionUsuario').getContext('2d');
    var formacionChart = new Chart(ctx2, {
        type: 'bar',
        data: {
            labels: etiquetas,
            datasets: [{
                label: 'Horas de Formación',
                data: horas,
                backgroundColor: 'rgba(0, 123, 255, 0.5)',
                borderColor: 'rgba(0, 123, 255, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

// Llamar a la función para crear el gráfico de horas de formación por área cuando el DOM esté listo
document.addEventListener('DOMContentLoaded', function () {
    const usuarioId = 1; // Cambia esto según el usuario que desees mostrar
    crearGraficoFormacionPorArea(usuarioId); // Cargar el gráfico de formación por área al inicio
});