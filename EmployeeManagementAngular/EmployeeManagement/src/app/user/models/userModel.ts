// src/app/user/models/userModel.ts
export interface RegisterUserDTO {
  username: string;
  email: string;
  password: string;
  dateOfBirth?: Date;
}

export interface LoginUserDTO {
  email: string;
  password: string;
}

export interface TokenResponse {
  token: string;
}

export interface User {
  userId: number;
  username: string;
  email: string;
  createdAt?: Date;
}
