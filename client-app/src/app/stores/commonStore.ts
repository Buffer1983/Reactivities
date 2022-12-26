import { tr } from "date-fns/locale";
import { makeAutoObservable, reaction } from "mobx";
import { ServerError } from "../models/serverError";

export default class CommonStore{
    error: ServerError | null = null;   
    token: string | null = localStorage.getItem('jwt');
    appLoaded = false;

    constructor(){
        makeAutoObservable(this);

        //Reaction (not autorun) when token is changing using Mobx.
        //It doesn't care about initial values
        //But if status change then expression will occur
        reaction(
            ()=> this.token,
            token=> {
                if(token){
                    localStorage.setItem('jwt',token)
                } else {
                    localStorage.removeItem('jwt')
                }
            }
        )
    }

    setServerError(error:ServerError){
        this.error = error;
    }

    setToken = (token: string | null) => {
        this.token =token;
    }

    setAppLoaded = () => {
        this.appLoaded =true;
    }

}

