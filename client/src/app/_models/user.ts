export interface User {
    id: number,
    name: string,
    email: string;
    token: string;
    password?: string;
    roles: string[];
}