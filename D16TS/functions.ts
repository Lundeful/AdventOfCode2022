export interface Valve {
    name: string;
    flowRate: number;
    connections: string[];
    isOpen: boolean;
    amountOfJumps?: number;
    visited?: boolean;
}

export const ParseInput = (lines: string[]): Valve[] => {
    return lines.map(line => {
        const parts = line.split(' ');
        const name = parts[1];
        const connections = parts.slice(9).map(s => s.replace(',', ''));
        const flowRate = Number.parseInt(parts[4].split('=')[1].split(';')[0]);

        const valve: Valve = {
            name: name,
            flowRate: flowRate,
            connections: connections,
            isOpen: false,
        };
        return valve;
    });
};
