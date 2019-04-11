export default class User {
    Username: string;
    Password: string;
    Name: string;
    Token: string;
    UserType: UserType
}

export enum UserType {
    General,
    Employee,
    HR,
    IT
}