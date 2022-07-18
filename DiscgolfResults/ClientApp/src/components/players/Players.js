import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import { NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import i18n from "../../i18n";

export class Players extends Component {
  static displayName = Players.name;

  constructor(props) {
    super(props);
    this.state = { 
        players: [],
        loading: true 
    };
  }

  componentDidMount() {
    this.populateResultData();
  }

  static renderPlayersTable(players) {
    return (
      <table className='table table-condensed table-striped table-sm' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>{i18n.t('column_name')}</th>
          </tr>
        </thead>
        <tbody>
          {players.map(x =>
            <tr key={x.playerId}>
                <td>                  
                  <NavLink href={"players/" + x.playerId } className="text-dark">{x.firstName + ' ' + x.lastName}</NavLink>
                </td>              
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {    
    let contents = this.state.loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : Players.renderPlayersTable(this.state.players);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('players_header')}</h1>
        <p>{i18n.t('players_description')}</p>        
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('/api/players');
    const data = await response.json();
    this.setState({ players: data, loading: false });
  }
}