export interface Device {
    id?: number;
    name: string;
    manufacturer: string;
    type: string;
    os: string;
    osversion: number;
    processor: number;
    ram: number;
    appUserId?: number;
    userName?: string;
}