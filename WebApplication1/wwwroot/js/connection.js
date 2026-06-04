const connection = new signalR.HubConnectionBuilder()
    .withUrl("/gameHub")
    .build()


connection.on("updateState", (state) => {
    updateGame(state)
})


connection.on("gameOver", () => {
    triggerDeath()
})


connection.on("gameWin", () => {
    showMessage("VOCÊ GANHOU! 🎉")
})


connection.start()
    .then(() => {
        console.log("Conectado ao servidor!")
        connection.invoke("GetState")
    })
    .catch(err => console.error("Erro ao conectar:", err))