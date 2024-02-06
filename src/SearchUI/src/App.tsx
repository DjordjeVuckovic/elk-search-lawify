import './App.scss'
import {QueryClient, QueryClientProvider} from "react-query";
import {Navbar} from "./shared/navbar";
import {Body} from "./shared/ui/body/body.tsx";
import {Route, Routes} from "react-router-dom";
import {Home} from "./home/home.tsx";
import {Laws} from "./laws/laws.tsx";
import {Contracts} from "./contracts/contracts.tsx";
import {LawPage} from "./laws/law-page.tsx";
import {Toaster} from "react-hot-toast";
import {ContractPage} from "./contracts/contract-page.tsx";
const queryClient = new QueryClient();
function App() {

  return (
    <>
        <QueryClientProvider client={queryClient}>
            <Navbar/>
            <Body>
                <Routes>
                    <Route path={'/'} element={ <Home/> } />
                    <Route path={'/laws'} element={ <Laws /> } />
                    <Route path={'/laws/item'} element={ <LawPage /> } />
                    <Route path={'/contracts'} element={ <Contracts /> } />
                    <Route path={'/contracts/item'} element={ <ContractPage /> } />
                </Routes>
            </Body>
            <Toaster position="bottom-center"
                     reverseOrder={false}
            />
        </QueryClientProvider>
    </>
  )
}

export default App
