export interface Shape {
    name: string;
    lines: Coordinate[];
    currentStartingCoordinate?: { x: number; y: number };
    width: number;
    height: number;
}

export interface Coordinate {
    x: number;
    y: number;
}

export const FlatLine: Shape = {
    name: 'FlatLine',
    lines: [
        { x: 0, y: 0 },
        { x: 1, y: 0 },
        { x: 2, y: 0 },
        { x: 3, y: 0 },
    ],
    width: 4,
    height: 1,
};

export const Plus: Shape = {
    name: 'Plus',
    lines: [
        { x: 1, y: 0 },
        { x: 0, y: 1 },
        { x: 1, y: 1 },
        { x: 2, y: 1 },
        { x: 1, y: 2 },
    ],
    width: 3,
    height: 3,
};

export const L: Shape = {
    name: 'L',
    lines: [
        { x: 2, y: 0 },
        { x: 2, y: 1 },
        { x: 2, y: 2 },
        { x: 1, y: 2 },
        { x: 0, y: 2 },
    ],
    width: 3,
    height: 3,
};

export const Square: Shape = {
    name: 'Square',
    lines: [
        { x: 0, y: 0 },
        { x: 1, y: 0 },
        { x: 0, y: 1 },
        { x: 1, y: 1 },
    ],
    width: 2,
    height: 2,
};

export const StandingLine: Shape = {
    name: 'StandingLine',
    lines: [
        { x: 0, y: 0 },
        { x: 0, y: 1 },
        { x: 0, y: 2 },
        { x: 0, y: 3 },
    ],
    width: 1,
    height: 4,
};

export const Shapes = [FlatLine, Plus, L, StandingLine];
