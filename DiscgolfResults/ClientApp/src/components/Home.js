import React, { Component } from 'react';
import i18n from "../i18n";
import 'bootstrap/dist/css/bootstrap.min.css';
//import logo from '../images/Logo_OrbitTibro_svart_300dpi.jpg';

export class Home extends Component {
  static displayName = Home.name;

  //<img src={logo} width="100%" alt="orbiTibro"/>

  render () {
    return (
      <div>
        <h1>{i18n.t('home_header')}</h1>
        <p>{i18n.t('home_description')}</p>        
      </div>
    );
  }
}
