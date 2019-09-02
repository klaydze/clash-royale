// export class Card {
//     _id: string;
//     idName: string;
//     rarity: string;
//     type: string;
//     name: string;
//     description: string;
//     elixirCost: number;
//     copyId: number;
//     arena: number;
//     order: number;
//     __v: number;
//     imageUrl: string;
// }

export class Card {
    id: string;
    idName: string;
    rarity: string;
    type: string;
    name: string;
    description: string;
    elixirCost: number;
    version: number;
    imageUrl: string;
}

export class CardDetail {
    constructor() {
        this.troops = [];
        this.buildings = [];
        this.spells = [];
    }

    troops: Card[];
    buildings: Card[];
    spells: Card[];
}