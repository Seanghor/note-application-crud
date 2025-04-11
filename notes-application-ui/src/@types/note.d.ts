import { User } from './user.d.ts';
export interface CreateNoteDto {
    title: string;
    content?: string;
}

export interface UpdateNoteDto {
    title: string;
    content: string;
}

export interface NoteResponse {
    id: number;
    title: string;
    content: string;
    created_At: string | Date;
    updated_At: string | Date | null;
    userId: number;
    user?:User | null; // Optional user property
}