// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener("DOMContentLoaded", function () {
    const textElement = document.querySelector('.progress-text');
    textElement.addEventListener('animationstart', () => {
        textElement.classList.add('animate-text');
    });

    textElement.addEventListener('animationend', () => {
        textElement.classList.remove('animate-text');
    });
});


$("form").submit(function (e) {
    e.preventDefault();

    var selectedValues = [];
    $("select").each(function () {
        var value = $(this).val();
        if (value) {
            selectedValues.push(value);
        }
    });

    // Concatena os valores selecionados em uma string separada por vírgula
    var ambientesSelecionados = selectedValues.join(", ");

    // Atribui a string concatenada ao campo 'Descricao' (é o campo do formulário)
    $("input[name='Descricao']").val(ambientesSelecionados);

    // Agora submete o formulário
    this.submit();
});

$("form").submit(function (e) {
    e.preventDefault();

    var selectedValues = [];
    $("select").each(function () {
        var value = $(this).val();
        if (value) {
            selectedValues.push(value);
        }
    });

    // Concatena os valores selecionados em uma string separada por vírgula
    var itensSelecionados = selectedValues.join(", ");

    // Atribui a string concatenada ao campo 'Descricao' (é o campo do formulário)
    $("input[name='DescricaoItem']").val(itensSelecionados);

    // Agora submete o formulário
    this.submit();
});


// Mapa

// Dados fictícios de consumo por cômodo
const roomData = {
    "sala": {
        consumo: 120,
        itens: [
            { nome: "Ar-condicionado", consumo: 80 },
            { nome: "TV", consumo: 40 }
        ]
    },
    "cozinha": {
        consumo: 150,
        itens: [
            { nome: "Geladeira", consumo: 100 },
            { nome: "Micro-ondas", consumo: 50 }
        ]
    },
    "quarto1": {
        consumo: 90,
        itens: [
            { nome: "Luz", consumo: 10 },
            { nome: "Ar-condicionado", consumo: 80 }
        ]
    },
    "quarto2": {
        consumo: 80,
        itens: [
            { nome: "Luz", consumo: 10 },
            { nome: "Computador", consumo: 70 }
        ]
    },
    "banheiro": {
        consumo: 50,
        itens: [
            { nome: "Luz", consumo: 20 },
            { nome: "Aquecedor", consumo: 30 }
        ]
    },
    "lavanderia": {
        consumo: 40,
        itens: [
            { nome: "Máquina de lavar", consumo: 40 }
        ]
    }
};

// Adicionando interatividade aos cômodos e itens
document.querySelectorAll('.room').forEach(room => {
    room.addEventListener('click', function () {
        const roomName = this.getAttribute('data-room');
        showPopup(roomName);
    });
});

document.querySelectorAll('.item').forEach(item => {
    item.addEventListener('click', function (event) {
        event.stopPropagation();
        const itemName = this.getAttribute('data-item');
        const roomName = this.parentElement.getAttribute('data-room');
        showPopup(roomName, itemName);
    });
});

// Mostrar popup com detalhes
function showPopup(roomName, itemName = null) {
    const room = roomData[roomName];
    document.getElementById('room-title').textContent = roomName.charAt(0).toUpperCase() + roomName.slice(1);
    document.getElementById('energy-consumption').textContent = room.consumo;

    const itemList = document.getElementById('items-list');
    itemList.innerHTML = '';
    room.itens.forEach(item => {
        const listItem = document.createElement('li');
        listItem.textContent = `${item.nome}: ${item.consumo} kWh`;
        itemList.appendChild(listItem);
    });

    if (itemName) {
        alert(`Item: ${itemName}`);
    }

    document.getElementById('popup').classList.add('active');
}

// Fechar popup
function closePopup() {
    document.getElementById('popup').classList.remove('active');
}

// Gráfico diário

document.addEventListener('DOMContentLoaded', function () {
    const consumoData = JSON.parse(document.getElementById('consumoData').getAttribute('data-consumo'));

    //const labels = consumoData.map(c => c.DataConsumo);
    const groupedData = consumoData.reduce((acc, current) => {
        const date = new Date(current.DataConsumo);
        const day = date.getDate();  // Obtém o dia do mês
        
        if (!acc[day]) {
            acc[day] = { consumo: 0 };  // Se não existir, inicializa
        }

        // Somar os consumos do mesmo dia
        acc[day].consumo += current.ConsumoDiario;
        
        return acc;
    }, {});

    // Ordenar os dias em ordem crescente
    const sortedDays = Object.keys(groupedData).sort((a, b) => a - b);

    // Preparar os dados para o gráfico
    const labels = sortedDays;

    const consumoValores = consumoData.map(c => c.ConsumoDiario); 

    // Configuração do gráfico
    const ctx = document.getElementById('consumoChart').getContext('2d');
    const consumoChart = new Chart(ctx, {
        type: 'bar',  
        data: {
            labels: labels,  
            datasets: [{
                label: 'Consumo Diário',
                data: consumoValores,  
                backgroundColor: 'rgba(75, 192, 192, 0.2)',  // Cor das barras
                borderColor: 'rgba(75, 192, 192, 1)',  // Cor da borda das barras
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
});

// Gráfico de pizza
document.addEventListener('DOMContentLoaded', function () {
    const roomData = {
        "sala": {
            consumo: 120,
            itens: [
                { nome: "Ar-condicionado", consumo: 80},
                { nome: "TV", consumo: 40  }
                
            ]
        },
        "cozinha": {
            consumo: 150,
            itens: [
                { nome: "Geladeira", consumo: 100 },
                { nome: "Micro-ondas", consumo: 50 }
            ]
        },
        "quarto1": {
            consumo: 90,
            itens: [
                { nome: "Luz", consumo: 10 },
                { nome: "Ar-condicionado", consumo: 80 }
            ]
        },
        "quarto2": {
            consumo: 80,
            itens: [
                { nome: "Luz", consumo: 10 },
                { nome: "Computador", consumo: 70 }
            ]
        },
        "banheiro": {
            consumo: 50,
            itens: [
                { nome: "Luz", consumo: 20 },
                { nome: "Aquecedor", consumo: 30 }
            ]
        },
        "lavanderia": {
            consumo: 40,
            itens: [
                { nome: "Máquina de lavar", consumo: 40 }
            ]
        }
    };

    // Preparar dados para o gráfico de pizza
    const roomLabels = Object.keys(roomData);
    const roomConsumos = roomLabels.map(room => roomData[room].consumo);

    // Configuração do gráfico de pizza
    const ctx = document.getElementById('consumoPizzaChart').getContext('2d');
    const consumoPizzaChart = new Chart(ctx, {
        type: 'pie',  // Tipo do gráfico: Pizza
        data: {
            labels: roomLabels,  // Nomes dos ambientes
            datasets: [{
                label: 'Consumo por Ambiente',
                data: roomConsumos,  // Dados de consumo para cada ambiente
                backgroundColor: [
                    'rgba(75, 192, 192, 0.5)', // Sala
                    'rgba(255, 99, 132, 0.5)', // Cozinha
                    'rgba(54, 162, 235, 0.5)', // Quarto 1
                    'rgba(255, 159, 64, 0.5)', // Quarto 2
                    'rgba(153, 102, 255, 0.5)', // Banheiro
                    'rgba(255, 206, 86, 0.5)'  // Lavanderia
                ],  // Cores para cada segmento
                borderColor: [
                    'rgba(75, 192, 192, 1)', // Sala
                    'rgba(255, 99, 132, 1)', // Cozinha
                    'rgba(54, 162, 235, 1)', // Quarto 1
                    'rgba(255, 159, 64, 1)', // Quarto 2
                    'rgba(153, 102, 255, 1)', // Banheiro
                    'rgba(255, 206, 86, 1)'  // Lavanderia
                ],  // Cores da borda dos segmentos
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: 'top',
                },
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            const roomName = tooltipItem.label;
                            const roomConsumption = tooltipItem.raw;
                            return `${roomName}: ${roomConsumption} kWh`;
                        }
                    }
                }
            }
        }
    });
});


document.addEventListener('DOMContentLoaded', function () {
    // Exemplo de dados fictícios mensais de consumo em kWh (simulando consumo por mês)
    const consumoMensalData = [
        { mes: "Janeiro", consumo: 320 },
        { mes: "Fevereiro", consumo: 280 },
        { mes: "Março", consumo: 350 },
        { mes: "Abril", consumo: 400 },
        { mes: "Maio", consumo: 300 },
        { mes: "Junho", consumo: 330 },
        { mes: "Julho", consumo: 310 },
        { mes: "Agosto", consumo: 340 },
        { mes: "Setembro", consumo: 330 },
        { mes: "Outubro", consumo: 360 },
        { mes: "Novembro", consumo: 380 },
        { mes: "Dezembro", consumo: 370 }
    ];

    // Vamos calcular o valor da conta de luz mensal (supondo que cada kWh custa R$ 0,60)
    const valorPorKWh = 0.60; // Preço por kWh
    const valoresContaLuz = consumoMensalData.map(item => item.consumo * valorPorKWh);

    // Extrair os meses e valores para o gráfico
    const meses = consumoMensalData.map(item => item.mes);
    const valores = valoresContaLuz;

    // Configuração do gráfico de barras
    const ctx = document.getElementById('consumoMensalChart').getContext('2d');
    const consumoMensalChart = new Chart(ctx, {
        type: 'bar',  // Tipo do gráfico: barras
        data: {
            labels: meses,  // Rótulos dos meses
            datasets: [{
                label: 'Valor da Conta de Luz (R$)',
                data: valores,  // Valores das contas de luz mensais
                backgroundColor: 'rgba(100, 192, 100, 0.2)',  // Cor das barras
                borderColor: 'rgba(100, 192, 100, 1)',  // Cor da borda das barras
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        callback: function (value) { return 'R$ ' + value.toFixed(2); } // Exibe o valor em R$
                    }
                }
            },
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function (tooltipItem) {
                            return 'R$ ' + tooltipItem.raw.toFixed(2); // Exibe o valor formatado nos tooltips
                        }
                    }
                }
            }
        }
    });
});

// Script para o carrossel
let currentSlide = 0;
const slides = document.querySelectorAll('.carousel-slide');
const dots = document.querySelectorAll('.dot');

function moveToSlide(index) {
    const slideContainer = document.querySelector('.carousel-slide-container');
    slideContainer.style.transform = `translateX(-${index * 100}%)`;

    // Atualiza a opacidade das bolinhas de navegação
    dots.forEach((dot, i) => {
        dot.style.opacity = i === index ? '1' : '0.5';
    });

    currentSlide = index;
}

// Avançar para o próximo slide a cada 5 segundos
setInterval(() => {
    currentSlide = (currentSlide + 1) % slides.length;
    moveToSlide(currentSlide);
}, 8000);


// Chat


function startRecognition() {
    const recognition = new (window.SpeechRecognition || window.webkitSpeechRecognition)();
    recognition.lang = 'pt-BR';
    recognition.start();

    recognition.onresult = function(event) {
        const transcript = event.results[0][0].transcript;
        addMessage(transcript, true);
        sendMessage(transcript);
    };
}

async function toggleChat() {
    const chatContainer = document.getElementById("chat-container");
    chatContainer.classList.toggle("open");

    if (chatContainer.classList.contains("open")) {
        try {
            const response = await fetch("/Chat/GetMenu");
            if (!response.ok) {
                throw new Error("Erro ao carregar o menu.");
            }

            const menuData = await response.json();
            displayMenu(menuData.response);
        } catch (error) {
            console.error("Erro:", error);
            displayMenu("Erro ao carregar o menu.");
        }
    }
}

function handleKeyPress(event) {
    if (event.key === "Enter") {
        sendMessage();
    }
}

function displayMenu(menu) {
    const chatBody = document.getElementById("chat-body");
    const formattedMenu = menu.replace(/\n/g, "<br>");
    chatBody.innerHTML = `<p class="chat-message">${formattedMenu}</p>`;
}

function sendMessage(message) {
let input = document.getElementById("chatInput");
if (!message) {
    message = input.value.trim();
}

if (message) {
    addMessage(message, true);

    fetch('/Chat/SendMessage', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ text: message })
    })
    .then(response => response.json())
    .then(data => {
        addMessage(data.response, false);
    });

    input.value = "";
}
}

function addMessage(text, isUser) {
    let chatBody = document.getElementById("chat-body");
    let messageDiv = document.createElement("div");
    messageDiv.className = isUser ? "user-message" : "bot-message";
    messageDiv.innerText = text;
    chatBody.appendChild(messageDiv);
    chatBody.scrollTop = chatBody.scrollHeight;
}

function playAudioResponse(audioUrl) {
    const audio = new Audio(audioUrl);
    audio.play();
}
