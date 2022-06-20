import Layout from './Components/Layout';
import React, { Component } from 'react';
import { Route } from 'react-router';
import Home from './Pages/Home';
import PrivateRoute from './Components/PrivateRoute';
import { AuthContextComponent } from './AuthContext';
import Signup  from './Pages/Signup';
import Login from './Pages/Login';
import Logout from './Pages/Logout';



export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <AuthContextComponent>
      <Layout>
       <PrivateRoute exact path='/' component={Home} />
        <Route exact path='/signup' component={Signup}/>
        <Route exact path='/login' component={Login}/>
        <Route exact path='/logout' component={Logout}/>
       
      </Layout>
      </AuthContextComponent>
    );
  }
}
