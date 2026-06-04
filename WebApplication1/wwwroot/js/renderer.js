const TILE = 28
const ROWS = 22
const COLS = 19

const canvas = document.getElementById('gameCanvas')
const ctx = canvas.getContext('2d')

canvas.width = COLS * TILE
canvas.height = ROWS * TILE

const maze = [
    [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
    [1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1],
    [1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1],
    [1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1],
    [1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1],
    [1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1],
    [1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1],
    [1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1],
    [1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1],
    [1, 1, 1, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1],
    [0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0],
    [1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1],
    [1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1],
    [1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1],
    [1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1],
    [1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1],
    [1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1],
    [1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1],
    [1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1],
    [1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1],
    [1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1],
    [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1]
]


let lastState = null
let prevPacman = { row: 20, col: 9 }
let prevGhosts = []
let interpProgress = 0  
const INTERP_SPEED = 0.15 


let deathAnimation = false
let deathProgress = 0  
let deathX = 0
let deathY = 0


let mouthAngle = 0.2
let mouthDir = 1

function animateMouth() {
    mouthAngle += mouthDir * 0.05
    if (mouthAngle > 0.6) mouthDir = -1
    if (mouthAngle < 0.05) mouthDir = 1
}


function lerp(a, b, t) {
    return a + (b - a) * t
}


function drawMaze() {
    for (let r = 0; r < maze.length; r++) {
        for (let c = 0; c < maze[r].length; c++) {
            if (maze[r][c] === 1) {
                ctx.fillStyle = '#1a1aff'
                ctx.beginPath()
                ctx.roundRect(c * TILE + 1, r * TILE + 1, TILE - 2, TILE - 2, 4)
                ctx.fill()
            } else {
                ctx.fillStyle = '#000'
                ctx.fillRect(c * TILE, r * TILE, TILE, TILE)
            }
        }
    }
}


function drawDots(dots) {
    dots.forEach(nodeId => {
        const row = Math.floor(nodeId / COLS)
        const col = nodeId % COLS
        const x = col * TILE + TILE / 2
        const y = row * TILE + TILE / 2

        ctx.beginPath()
        ctx.arc(x, y, 3, 0, Math.PI * 2)
        ctx.fillStyle = '#FFD700'
        ctx.fill()
    })
}


function drawPacman(pacman) {
    const x = lerp(
        prevPacman.col * TILE + TILE / 2,
        pacman.col * TILE + TILE / 2,
        interpProgress
    )
    const y = lerp(
        prevPacman.row * TILE + TILE / 2,
        pacman.row * TILE + TILE / 2,
        interpProgress
    )

    animateMouth()

    
    const dx = pacman.col - prevPacman.col
    const dy = pacman.row - prevPacman.row
    let rotation = 0
    if (dx === 1) rotation = 0
    if (dx === -1) rotation = Math.PI
    if (dy === 1) rotation = Math.PI / 2
    if (dy === -1) rotation = -Math.PI / 2

    ctx.save()
    ctx.translate(x, y)
    ctx.rotate(rotation)
    ctx.beginPath()
    ctx.moveTo(0, 0)
    ctx.arc(0, 0, TILE / 2 - 4, mouthAngle, Math.PI * 2 - mouthAngle)
    ctx.closePath()
    ctx.fillStyle = '#FFE000'
    ctx.fill()
    ctx.restore()
}


function drawDeathAnimation() {
    deathProgress += 0.03

    const radius = (TILE / 2 - 4) * (1 - deathProgress)
    const angle = deathProgress * Math.PI

    if (radius > 0) {
        ctx.save()
        ctx.translate(deathX, deathY)
        ctx.beginPath()
        ctx.moveTo(0, 0)
        ctx.arc(0, 0, radius, angle, Math.PI * 2 - angle)
        ctx.closePath()
        ctx.fillStyle = '#FFE000'
        ctx.fill()
        ctx.restore()
    }

    if (deathProgress >= 1) {
        deathAnimation = false
        deathProgress = 0
        showMessage("GAME OVER!")
    }
}


function drawGhosts(ghosts) {
    ghosts.forEach((ghost, i) => {
        const prev = prevGhosts[i] || ghost

        const x = lerp(
            prev.col * TILE + 2,
            ghost.col * TILE + 2,
            interpProgress
        )
        const y = lerp(
            prev.row * TILE + 2,
            ghost.row * TILE + 2,
            interpProgress
        )

        const w = TILE - 4
        const h = TILE - 4

        
        ctx.fillStyle = ghost.color
        ctx.beginPath()
        ctx.arc(x + w / 2, y + h * 0.45, w / 2, Math.PI, 0)
        ctx.lineTo(x + w, y + h)

        
        const waveSegs = 3
        for (let s = waveSegs; s >= 0; s--) {
            const wx = x + w * (s / waveSegs)
            const wy = s % 2 === 0 ? y + h : y + h - h * 0.2
            ctx.lineTo(wx, wy)
        }

        ctx.lineTo(x, y + h)
        ctx.closePath()
        ctx.fill()

        
        ctx.fillStyle = '#fff'
        ctx.beginPath()
        ctx.arc(x + w * 0.35, y + h * 0.4, w * 0.14, 0, Math.PI * 2)
        ctx.fill()
        ctx.beginPath()
        ctx.arc(x + w * 0.65, y + h * 0.4, w * 0.14, 0, Math.PI * 2)
        ctx.fill()

        
        ctx.fillStyle = '#00f'
        ctx.beginPath()
        ctx.arc(x + w * 0.38, y + h * 0.42, w * 0.07, 0, Math.PI * 2)
        ctx.fill()
        ctx.beginPath()
        ctx.arc(x + w * 0.68, y + h * 0.42, w * 0.07, 0, Math.PI * 2)
        ctx.fill()
    })
}


function updateGame(state) {
    if (lastState) {
        prevPacman = { row: lastState.pacman.row, col: lastState.pacman.col }
        prevGhosts = lastState.ghosts.map(g => ({ row: g.row, col: g.col }))
    }
    lastState = state
    interpProgress = 0
}


function gameLoop() {
    ctx.clearRect(0, 0, canvas.width, canvas.height)
    drawMaze()

    if (lastState) {
        drawDots(lastState.dots)
        document.getElementById('score').textContent = lastState.pacman.score

        if (deathAnimation) {
            drawDeathAnimation()
        } else {
            interpProgress = Math.min(interpProgress + INTERP_SPEED, 1)
            drawPacman(lastState.pacman)
            drawGhosts(lastState.ghosts)
        }
    }

    requestAnimationFrame(gameLoop)
}

gameLoop()


function showMessage(text) {
    document.getElementById('messageText').textContent = text
    document.getElementById('message').classList.remove('hidden')
}


function triggerDeath() {
    if (lastState) {
        deathX = lastState.pacman.col * TILE + TILE / 2
        deathY = lastState.pacman.row * TILE + TILE / 2
        deathAnimation = true
        deathProgress = 0
    }
}