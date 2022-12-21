export interface Shape {
    name: string;
    lines: (pos: Coordinate) => Coordinate[];
    pos: Coordinate;
    isActive: boolean;
}

export interface Coordinate {
    x: number;
    y: number;
}

export const FlatLine: Shape = {
    name: 'FlatLine',
    lines: ({ x, y }: Coordinate) => {
        return [
            { x: x + 0, y: y + 0 },
            { x: x + 1, y: y + 0 },
            { x: x + 2, y: y + 0 },
            { x: x + 3, y: y + 0 },
        ];
    },
    pos: { x: -1, y: 1 },
    isActive: false,
};

export const Plus: Shape = {
    name: 'Plus',
    lines: ({ x, y }: Coordinate) => {
        return [
            { x: x + 1, y: y + 0 },
            { x: x + 0, y: y + 1 },
            { x: x + 1, y: y + 1 },
            { x: x + 2, y: y + 1 },
            { x: x + 1, y: y + 2 },
        ];
    },
    pos: { x: -10, y: -10 },
    isActive: false,
};

export const L: Shape = {
    name: 'L',
    lines: ({ x, y }: Coordinate) => {
        return [
            { x: x + 2, y: y + 0 },
            { x: x + 2, y: y + 1 },
            { x: x + 2, y: y + 2 },
            { x: x + 1, y: y },
            { x: x + 0, y: y },
        ];
    },
    pos: { x: -1, y: 1 },
    isActive: false,
};

export const Square: Shape = {
    name: 'Square',
    lines: ({ x, y }: Coordinate) => {
        return [
            { x: x + 0, y: y + 0 },
            { x: x + 1, y: y + 0 },
            { x: x + 0, y: y + 1 },
            { x: x + 1, y: y + 1 },
        ];
    },
    pos: { x: -1, y: 1 },
    isActive: false,
};

export const StandingLine: Shape = {
    name: 'StandingLine',
    lines: ({ x, y }: Coordinate) => {
        return [
            { x: x + 0, y: y + 0 },
            { x: x + 0, y: y + 1 },
            { x: x + 0, y: y + 2 },
            { x: x + 0, y: y + 3 },
        ];
    },
    pos: { x: -1, y: 1 },
    isActive: false,
};

export const Shapes = [FlatLine, Plus, L, StandingLine, Square];
