import fs from 'fs';
import { Coordinate, Shape, Shapes } from './shapes';

console.clear();
console.log('\n#################### Running program ####################');
const moves = fs.readFileSync('./testInput.txt').toString().split('');
const spawnCoordinate: Coordinate = { x: 3, y: 0 };
const shapes = Shapes;
const board: string[][] = [[]];

const FillStartingBoard = () => {
    for (let row = 0; row < 300; row++) {
        board[row] = [];
        for (let col = 0; col < 7; col++) {
            const el = '.';
            board[row][col] = el;
        }
    }
};

const cycleShapes = (): void => {
    shapes.push(shapes.shift()!);
};

const PlacePiece = () => {
    const nextShape = shapes[0];
    const pos = nextPieceStartingPos(board);
    if (nextShape.height > 1) pos.y += nextShape.height - 1;

    nextShape.lines.forEach(line => {
        const x = line.x + pos.x;
        const y = line.y + pos.y;
        board[y][x] = '@';
    });
    cycleShapes();
};

const nextPieceStartingPos = (board: string[][]): Coordinate => {
    let brickCoordinate: Coordinate | null = null;
    for (let row = board.length - 1; row >= 0 && brickCoordinate != null; row--) {
        for (let col = 0; col < 7 && brickCoordinate != null; col++) {
            const element = board[row][col];
            if (element === '#') {
                brickCoordinate = { x: col, y: row };
            }
        }
    }

    if (brickCoordinate == null || board.length <= 3) {
        // Empty board
        return { x: 2, y: 3 };
    }

    const nextPos = { x: 2, y: brickCoordinate.y + 3 };

    return nextPos!;
};

const movePieceSideways = (direction: string) => {
    if (direction == '>') {
        const canMoveRight = checkIfCanMoveRight();
        if (canMoveRight) {
            // Move right
        }
    } else if (direction == '<') {
        const canMoveLeft = checkIfCanMoveLeft();
        if (canMoveLeft) {
            // Move left
        }
    }
};

const movePieceDown = () => {
    const coordinates = [];
    for (let i = 0; i < board.length; i++) {
        for (let j = 0; j < board[i].length; j++) {
            if (board[i][j] === '@') {
                coordinates.push({ x: i, y: j });
            }
        }
    }

    if (coordinates.some(c => c.y - 1 == 0)) {
        // Reaches bottom
        board.forEach(l => l.forEach(c => c.replace('@', '#')));
        let didHit = false;
        coordinates.forEach(c => {
            if (!didHit) {
                const existingEl = board[c.y - 1][c.x];
                if (existingEl == '#') {
                    didHit = true;
                }
            }
        });
    } else {
        // Move down
    }
};

const printBoard = (move: string) => {
    console.log('\n\n### Current board ###');
    let content = '';
    for (let i = board.length - 1; i >= 0; i--) {
        const line = board[i];
        for (let j = 0; j < line.length; j++) {
            const element = line[j];
            content += ` ${element} `;
        }
        content += '\n';
    }

    content += 'Moved ' + move;

    console.log(content);
};

FillStartingBoard();
printBoard('nowhere');

const PlayRound = () => {
    if (!board.some(row => row.some(col => col == '@'))) {
        // No active pieces
        PlacePiece();
    }
    const move = moves.shift();
    if (move) {
        movePieceSideways(move);
    }
    movePieceDown();
    printBoard(move!);
};

const PlayGame = async () => {
    while (moves.length > 0) {
        PlayRound();
        const delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms));
        await delay(1000);
    }
};

PlayGame();

function checkIfCanMoveRight() {
    return true;
}

function checkIfCanMoveLeft() {
    // TODO:
    return true;
}

function checkIfCanMoveDown(coordinates: Coordinate[]) {
    

}