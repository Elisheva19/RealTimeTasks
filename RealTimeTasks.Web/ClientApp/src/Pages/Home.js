import React,  { useState, useEffect, useRef } from "react";
import { HubConnectionBuilder } from '@microsoft/signalr';
import axios from 'axios';
import { useAuthContext } from '../AuthContext';

  const Home=()=>{

    const [taskName, setTaskName]= useState('')
    const [tasks, setTasks]= useState([])
    const connectionRef=useRef(null)
    const {user}= useAuthContext();

    useEffect(()=>{

      const connectToHub = async ()=>{
        const connection= new HubConnectionBuilder().withUrl("/taskchat").build();
        await connection.start();
        connection.invoke('newLogin');
        connectionRef.current= connection;

        connection.on('newlogin', tasks=>{
          setTasks(tasks)
        })

        connection.on('newtask', newtasktoadd=>{
          setTasks(all=>[...all, newtasktoadd])
        })

        connection.on('statusupdate', tasks=>{
          setTasks(tasks)
        })  

     
      }

    connectToHub();
  }, [])


const markTakenClick= async (id)=>{
  await axios.post('/api/task/marktaken', {id})
}
const markFinishedClick= async (id)=>{
  await axios.post('/api/task/markfinished', {id})
}

 const onAddClick =async()=>{
await axios.post('/api/task/addtask', {taskName});
 }
    return (
      <div className="container mt-5">
       <div  className=" row">
         <div className="col-md-8">
           <input type="text" onChange={e=>{setTaskName(e.target.value)}} placeholder="Title" value={taskName} className="form-control"></input>
         </div>
      
      <div className="col-md-2">
        
            <button onClick={onAddClick}   className="btn btn-primary col-md-4">Add </button>
            </div>
            </div>
        
    <br />

    <table className="table table-hover table-striped table-bordered">
        <thead>
            <tr>
            <th>Title:</th>
            <th>Status:</th>
           
         
            </tr>

        </thead>
        <tbody>
{tasks.map(t=> <tr key={t.id}> 
  <td>{t.title}</td>

{t.taskStatus=== 0 && <td>
    <button onClick={()=>markTakenClick(t.id)} className="btn btn-primary"> Available</button>
   </td>}

{t.userId === user.id &&  <td>
 <button onClick={()=>markFinishedClick(t.id)} className="btn btn-danger"> I'm Done!!!  </button> </td>
  }

{t.userId !== user.id && t.taskStatus === 1 && <td>
  <button disabled className="btn btn-info"> {t.user.firstName} {t.user.lastName}  is doing this one</button> </td>
  }



  
</tr>)}

        </tbody>
      </table>
      </div>
    )
  }
export default Home;
