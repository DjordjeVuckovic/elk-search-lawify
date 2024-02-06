import {create} from "zustand";
import {ContractHit} from "../types/contract.ts";

interface ContractState {
    contract: ContractHit;
    setContract: (file: ContractHit) => void;
}

const useContractStore = create<ContractState>((set) => ({
    contract: {
        id: '',
        content: '',
        fileName: '',
        metadata: {}
    },
    setContract: (ch) => set(state => ({...state, contract: ch}))
}));

export default useContractStore;