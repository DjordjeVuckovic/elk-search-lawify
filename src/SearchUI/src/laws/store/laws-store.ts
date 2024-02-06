import {create} from "zustand";
import {Law} from "../types/law.ts";

interface LawState {
    law: Law;
    setLaw: (file: Law) => void;
}

const useLawStore = create<LawState>((set) => ({
    law: {
        id: '',
        content: '',
        fileName: '',
        metadata: {}
    },
    setLaw: (law) => set(state => ({...state, law: law}))
}));

export default useLawStore;