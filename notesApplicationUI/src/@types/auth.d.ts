export type AuthRespond = {
    refreshToken: string;
    token: string;
    tokenExpires: number;
    user: User;
};


export interface SignInUserDto {
    username: string;
    password: string;
}

export interface SignUpUserDto {
    username: string;
    password: string
}