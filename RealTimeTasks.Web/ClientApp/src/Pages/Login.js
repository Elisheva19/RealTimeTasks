import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import axios from 'axios';
import { useAuthContext } from '../AuthContext';
import useForm from '../Hooks/UseForm';


const Login = () => {

const [formData, setFormData]= useForm({email:'', password:''});
const[isValidLogin, setIsValidLogin]= useState(true);
const {setUser}= useAuthContext();
const history=useHistory();

const onSubmit= async e =>{
e.preventDefault();
const {data}= await axios.post('api/account/login', formData);
const isValid=!!data;
setIsValidLogin(isValid);

if(isValid){
    setUser(data);
    
    history.push('/')
}
}





  return (
    <div className="row">
        <div className="col-md-6 offset-md-3 card card-body bg-light">
            <h3>Log in to your account</h3>
            {!isValidLogin && <span className='text-danger'>Invalid username/password. Please try again.</span>}
            <form onSubmit={onSubmit}>
                <input onChange={setFormData} value={formData.email} type="text" name="email" placeholder="Email" className="form-control" />
                <br />
                <input onChange={setFormData} value={formData.password} type="password" name="password" placeholder="Password" className="form-control" />
                <br />
                <button className="btn btn-primary">Login</button>
            </form>
            <Link to="/signup">Sign up for a new account</Link>
        </div>
    </div>
)
}
export default Login;