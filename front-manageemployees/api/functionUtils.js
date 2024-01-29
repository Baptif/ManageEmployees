export const getErrorMsg = (error) => {
    let msgErreur = "";
    console.log(error.response.status)
    if(error.response.status == 400){
        msgErreur = error.response.data
    }else if(error.response.status == 500){
        msgErreur = error.response.data.detail
    } else {
        msgErreur = "Il y a eu une erreur !"
    }
    return msgErreur
}