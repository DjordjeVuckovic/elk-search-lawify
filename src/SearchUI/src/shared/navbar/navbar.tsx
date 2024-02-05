import "./navbar.scss"
import logo from "../../assets/logo.png";
import {NavLink} from "./ui/nav-link.tsx";
import {useNavigate} from "react-router-dom";
export const Navbar = () => {
    const navigate = useNavigate()
    return (
     <nav className='navbar-wrapper'>
         <section className='padding-base inner-width navbar-container'>
             <div className={'flex-center logo-container'}>
                 <img src={logo}
                      alt={'logo'}
                      className={'image-logo'}
                      loading='lazy' onClick={() => navigate('')}
                 />
             </div>
             <div className='flex-center navbar-menu-middle'>
                 <NavLink url={''}>Home</NavLink>
                 {<NavLink url={'/laws'}>Laws</NavLink>}
                 {<NavLink url={'/contracts'}>Contracts</NavLink>}
             </div>
             <div className='flex-end navbar-menu-end'>

             </div>
         </section>
     </nav>
    );
}