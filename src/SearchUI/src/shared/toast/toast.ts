import toast from "react-hot-toast";

export const toastSuccess = (message: string) => {
    toast.success(message, {
        style: {
            borderRadius: '10px',
            background: '#333',
            color: '#fff',
        }
    })
}
export const toastError = (message: string) => {
    toast.error(message, {
        style: {
            borderRadius: '12px',
            background: '#333',
            color: '#fff',
        }
    })
}