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
    id: number;
    idName: string;
    rarity: string;
    type: string;
    name: string;
    description: string;
    elixirCost: number;
    version: number;
    imageUrl: string;
    targets: string;
    hitSpeed: number;
    count: number;
    range: string
    speed: string;
    radius: number;
    deployTime: number;
    lifetime: number;
    dashRange: string;
    projectileRange: number;
    cardStatistics: CardStatistics[] = [];
}

export class CardStatistics {
    cardLevel: number;
    hitPoints: number;
    damange: number;
    damagePerSecond: number;
    dashDamage: number;
    areaDamage: number;
    crownTowerDamage: number;
    chargeDamage: number;
    shieldHitpoints: number;
    duration: number;
    healingPerSecond: number;
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

export class SearchTermCardFilter {
    constructor() {
        this.searchTerm = '';
        this.rarity = 'all';
    }
    searchTerm: string;
    rarity: string;
}