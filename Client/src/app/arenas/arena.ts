class Donate {
    common: number;
    rare: number;
}

class Request {
    common: number;
    rare: number;
}

class Clan {
    donate: Donate;
    request: Request;
}

export class Arena {
    id: number;
    idName: string;
    number: number;
    name: string;
    victoryGold: number;
    minTrophies: number;
    imageUrl: string;
}

// export class Arena {
//     _id: string;
//     idName: string;
//     number: number;
//     name: string;
//     victoryGold: number;
//     minTrophies: number;
//     order: number;
//     __v: number;
//     leagues: string[];
//     cardUnlocks: string[];
//     chests: string[];
//     clan: Clan;
//     imageUrl: string;
// }
