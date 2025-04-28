import { ROUTE } from "@/constant/api-routes";
import axios from "../utils/axios-instance";
import { CreateNoteDto, NoteResponse, UpdateNoteDto } from "@/@types/note";
import { NoteQueryParams } from "@/@interface/noteQuery.interface";

// export const getAllNotes = async () => {
//     const response = await axios.get(ROUTE.GET_ALL_NOTES);
//     console.log("get all notes response", response);
//     return response || [] as NoteResponse[];
// }

export const getAllNotes = async ({
    search = '',
    titleFilter = '',
    sortBy = 'created_At',
    descending = false
}: {
    search: string;
    titleFilter: string;
    sortBy: string;
    descending: boolean;
}) => {
    const response = await axios.get<NoteResponse[]>(ROUTE.GET_ALL_NOTES, {
        params: {
            search,
            titleFilter,
            sortBy,
            descending
        }
    });
    console.log('get all notes response', response);
    return response || [] as NoteResponse[];

};

export const getOneNote = async (id: number) => {
    const response = await axios.get(`${ROUTE.GET_NOTE}/${id}`);
    console.log("get one note response", response);
    return response;
}

export const addNote = async (note: CreateNoteDto) => {
    const response = await axios.post(ROUTE.CREATE_NOTE, note);
    console.log("create note response", response);
    return response;
}

export const updateNoteById = async (id: number, note: UpdateNoteDto) => {
    const response = await axios.put(`${ROUTE.UPDATE_NOTE}/${id}`, note);
    console.log("update note response", response);
    return response;
}

export const deleteNoteById = async (id: number) => {
    const response = await axios.delete(`${ROUTE.DELETE_ONE}/${id}`);
    console.log("delete note response", response);
    return response;
}

export const deleteAllNotes = async () => {
    const response = await axios.delete(ROUTE.DELETE_ALL_NOTES);
    return response;
}