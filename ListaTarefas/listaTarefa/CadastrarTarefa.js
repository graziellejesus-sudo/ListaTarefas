const myForm1 = document.getElementById('Cadastrar');
if (myForm1 != null) {
myForm1.addEventListener('submit', function (event) {
    // 1. Prevenir o recarregamento da página ao submeter form
    event.preventDefault();

    fetch('https://localhost:7229/Tarefa/cadastrar', {
        method: 'POST', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            descricao: document.getElementById("descricao").value,
            status: document.getElementById("status").value
            
        }),
    }).then(response => {
        if (response.status ==401){
            alert ("Faça login antes de cadastrar!");
            window.location.href="index.html";
        }
        response.json();})
        .then(data => {
            document.getElementById("resposta").innerHTML ="<h4>Tarefa cadastrada com sucesso!</h4>";        
        })
});
}

fetch('https://localhost:7229/Tarefa',
    { 
        credentials: 'include' 
})
    
    .then(data => {
        if(data.length >0){
        var resposta = document.getElementById("respostaConsulta");
        resposta.innerHTML = "<h4>Segue Lista de suas tarefa</h4> ";
        for (i = 0; i < data.length; i++) {
            resposta.innerHTML += "<li> usuario: " + data[i].usuario+ "</li>";
            resposta.innerHTML += "Descricao: <input type='text' id='descricao"+data[i].tarefas+"' value='" + data[i].descricao + "'>";
            resposta.innerHTML += "status: <input type='text' id='status"+data[i].tarefas+"' value='" + data[i].status + "'>";
            resposta.innerHTML += "<button onclick='editaTarefa("+data[i].tarefas+")'>Editar Tarefa</button>";
            resposta.innerHTML += "<button onclick='deletaTarefa("+data[i].tarefas+")'>Deletar Tarefa </button> <hr>";

        }
    }
    });

    function deletaTarefa(idTarefa){
        fetch('https://localhost:7229/Tarefa/'+idTarefa, {
            method: 'DELETE', 
            credentials: 'include'
  
        }).then(response => {
            alert("Tarefa excluída");
            window.location.href="index.html";
        })
    }

    function editaTarefa (idTarefa){
        fetch('https://localhost:7229/Tarefa/'+idTarefa, {
            method: 'PUT',   
            credentials: 'include',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                descricao: document.getElementById("descricao"+descricao).value,
                status:document.getElementById("status"+idTarefa).value

            }),
        }).then(response => {
            if (response.status ==401){
                alert ("Faça login antes de editar!");
                window.location.href="index.html";
            }else{
                alert ("Tarefa editada!");
            }})
           
    }