const TOTAL_COLUNAS = 19
const MOVE_DELAY = 200

let pacmanRow = 20
let pacmanCol = 9
let lastMove = 0

connection.on("updateState", (state) => {
    pacmanRow = state.pacman.row
    pacmanCol = state.pacman.col
})

document.addEventListener('keydown', (e) => {
    const now = Date.now()
    if (now - lastMove < MOVE_DELAY) return
    lastMove = now

    let destRow = pacmanRow
    let destCol = pacmanCol

    if (e.key === 'ArrowRight') destCol += 1
    if (e.key === 'ArrowLeft') destCol -= 1
    if (e.key === 'ArrowDown') destRow += 1
    if (e.key === 'ArrowUp') destRow -= 1

    const destinationId = destRow * TOTAL_COLUNAS + destCol

    connection.invoke("MovePlayer", destinationId)
        .catch(err => console.error(err))
})

function restartGame() {
    document.getElementById('message').classList.add('hidden')
    connection.invoke("RestartGame")
        .catch(err => console.error(err))
}