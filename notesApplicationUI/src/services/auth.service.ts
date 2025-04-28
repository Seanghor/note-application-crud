import { SignInUserDto, SignUpUserDto } from "@/@types/auth";
import { ROUTE } from "@/constant/api-routes";
import axios from "../utils/axios-instance";
import { useRouter } from "vue-router";
const router = useRouter();




export const signUpAPI = async (signUpDto: SignUpUserDto) => {
    const response = await axios.post(ROUTE.SIGN_UP, {
        username: signUpDto.username,
        password: signUpDto.password,
    });
    return response;
}

export const signInAPI = async (signInDto: SignInUserDto) => {
    const response = await axios.post(ROUTE.SIGN_IN, {
        username: signInDto.username,
        password: signInDto.password,
    });
    return response;
}

