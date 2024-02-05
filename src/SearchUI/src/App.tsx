import './App.scss'
import {QueryClient, QueryClientProvider} from "react-query";
import {Navbar} from "./shared/navbar";
import {Body} from "./shared/body/body.tsx";
import {Route, Routes} from "react-router-dom";
import {Home} from "./home/home.tsx";
import {Laws} from "./laws/laws.tsx";
import {Contracts} from "./laws/contracts.tsx";
const queryClient = new QueryClient();
function App() {

  return (
    <>
        <QueryClientProvider client={queryClient}>
            <Navbar/>
            <Body>
                <Routes>
                    <Route path={'/'} element={ <Home/> }/>
                    <Route path={'/laws'} element={ <Laws/> }/>
                    <Route path={'/contracts'} element={ <Contracts/> }/>
                </Routes>
            </Body>
        </QueryClientProvider>
    </>
  )
}

export default App
