import * as fs from 'fs';
import { Coordinate, Shape, Shapes } from './shapes';

console.clear();
console.log('\n#################### Running program ####################');
const moves = fs.readFileSync('./input.txt').toString().split('');
const shapes = Shapes;
const pieces: Shape[] = [];
let inactivePieces = 0;

const PlacePiece = () => {
    const newShape = shapes.shift()!;
    shapes.push({ ...newShape });
    newShape.isActive = true;
    newShape.pos = getNextPieceStartingPos();
    pieces.unshift(newShape);
};

const getNextPieceStartingPos = () => {
    const xOffset = 2;
    const yOffset = 4;

    if (pieces.length == 0) {
        // Initial piece
        return { x: xOffset, y: 3 };
    }

    const positions = pieces.flatMap(p => p.lines(p.pos).map(p => p.y));
    const maxY = Math.max.apply(Math, positions);

    const yPos = maxY + yOffset;
    const newPos: Coordinate = { x: xOffset, y: yPos };
    return newPos;
};

const delay = async (ms: number = 100) => {
    return new Promise(resolve => {
        setTimeout(resolve, ms);
    });
};

const pieceIsOverlapping = (piece: Shape, pos: Coordinate) => {
    return piece.lines(piece.pos).some(l => l.x == pos.x && l.y == pos.y);
};

const printBoard = async (ms?: number) => {
    console.clear();
    if (pieces.length == 0) {
        console.log('Empty board');
        return;
    }

    const positions = pieces.flatMap(p => p.lines(p.pos).map(p => p.y));
    const maxY = Math.max.apply(Math, positions);

    let content = '';
    for (let y = 0; y < maxY + 1; y++) {
        for (let x = 0; x < 7; x++) {
            const activePiece = pieces.some(p => pieceIsOverlapping(p, { x, y }) && p.isActive);
            const inactivePiece = pieces.some(p => pieceIsOverlapping(p, { x, y }) && !p.isActive);

            if (activePiece) {
                content += '\x1b[41m  \x1b[0m';
            } else if (inactivePiece) {
                content += '\x1b[44m  \x1b[0m';
            } else {
                content += '\x1b[40m[]\x1b[0m';
            }
        }
        content += '\n';
    }

    const bottomBar = '\x1b[40m##############\x1b[0m\n';
    console.log(bottomBar + content);
    await delay(ms);
};

const canMove = (to: Coordinate): boolean => {
    const newPieces = [...pieces];
    const pieceToMove = newPieces.shift()!;
    const wantedPieceCoordinates = pieceToMove.lines(to);

    if (wantedPieceCoordinates.some(c => c.x > 6 || c.x < 0 || c.y < 0)) return false;

    const takenCoordinates = newPieces.flatMap(p => p.lines(p.pos));
    const isTaken = wantedPieceCoordinates.some(c1 => takenCoordinates.some(c2 => c1.x == c2.x && c1.y == c2.y));
    return !isTaken;
};

const moveSideways = (move: string) => {
    const dir = move == '<' ? -1 : 1;
    const activePiece = pieces[0];
    const wantedPosition: Coordinate = { x: activePiece.pos.x + dir, y: activePiece.pos.y };
    if (canMove(wantedPosition)) {
        activePiece.pos = wantedPosition;
    }
};

const moveDown = () => {
    const activePiece = pieces[0];
    const wantedPosition: Coordinate = { x: activePiece.pos.x, y: activePiece.pos.y - 1 };
    if (canMove(wantedPosition)) {
        activePiece.pos = wantedPosition;
    } else {
        activePiece.isActive = false;
        inactivePieces++;
    }
};

const PlayRound = async (move?: string) => {
    if (pieces.length == 0 || pieces.every(p => !p.isActive)) {
        PlacePiece();
        await printBoard(10);
        // console.log("yolo");
    }

    if (move) {
        moveSideways(move);
        await printBoard(3);
    }

    moveDown();
    await printBoard(3);
};

const PlayGame = async () => {
    console.log('Starting game');
    const part1 = 2022; // 3159
    const part2 = 1000000000000; // No chance lol

    var startTime = performance.now();
    while (inactivePieces < part1) {
        const nextMove = moves.shift()!;
        moves.push(nextMove);
        await PlayRound(nextMove);
    }

    await printBoard();
    printSummary();
};

const printSummary = () => {
    console.log('Amount of pieces', pieces.length);

    const allYCoordinates = pieces.flatMap(p => p.lines(p.pos).map(p => p.y));
    const maxY = Math.max.apply(Math, allYCoordinates);
    console.log('Units tall', maxY + 1);
};

PlayGame();
