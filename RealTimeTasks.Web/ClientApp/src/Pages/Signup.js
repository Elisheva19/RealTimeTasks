
import React  from 'react';
import useForm from '../Hooks/UseForm';
import { useHistory } from 'react-router-dom';
import axios from 'axios';

const Signup=()=>{
    const history= useHistory();
    const [formData, setFormData]= useForm({firstName:'', lastName: '', email:'', password:''});

    const onSubmitClick = async() => {
        await axios.post('/api/account/signup', formData);
        history.push('/login');
        
    }
return(
    <div className="col-md-6 offset-md-3 card card-body bg-light">
    <h1>Add Candidate:</h1>
    <br />
    <input type="text" value={formData.firstName} name='firstName' onChange={setFormData} className="form-control" placeholder="First Name" />
    <br />
    <input type="text" value={formData.lastName} name='lastName' onChange={setFormData} className="form-control" placeholder="Last Name" />
    <br />
    <input type="text" value={formData.email} name='email' onChange={setFormData} className="form-control" placeholder="Email" />
    <br />
    <input type="text" value={formData.password} name='password' onChange={setFormData} className="form-control" placeholder="Password" />
    <br />
    
    <button onClick={onSubmitClick} className="btn btn-primary btn-block">Submit</button>
</div>
)
}
export default Signup;