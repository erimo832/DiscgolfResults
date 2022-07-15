import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import i18n from "../../i18n";

export class PlayerInfo extends Component {
  static displayName = PlayerInfo.name;

  constructor(props) {
    super(props);
    var name = "";
    if(props.location && props.location.playerName)
        name = props.location.playerName.player;

    this.state = { 
        playerInfo: [],         
        name: name,
        loading: true 
    };
  }

  componentDidMount() {
    this.populateResultData();
  }

//npm i react-apexcharts
//chart.js https://www.npmjs.com/package/chart.js

  //Ugly substring to show date
  static renderPlayerInfoTable(info) {
    if(info.length === 0)
        return (<div>{i18n.t('playersinfo_no_found')}</div>);

    return (
        <div>
            <div></div>
            <table className='table table-condensed table-sm' aria-labelledby="tabelLabel">
            <thead>
                <tr>                    
                    <th className="d-none d-lg-table-cell">{i18n.t('column_round')}</th>
                    <th>{i18n.t('column_date')}</th>
                    <th>{i18n.t('column_place')}</th>
                    <th>{i18n.t('column_points')}</th>
                    <th>{i18n.t('column_score')}</th>
                    <th className="d-none d-sm-table-cell">{i18n.t('column_hcp')}</th>
                    <th className="d-none d-sm-table-cell">{i18n.t('column_hcpscore')}</th>
                </tr>
            </thead>
            <tbody>
                {info.events.map(x =>
                <tr key={x.time} className={x.inHcpAvgCalculation ? 'avg' : (x.inHcpCalculation === true ? 'top' : 'none' ) }>                    
                    <td className="d-none d-lg-table-cell">{x.eventName}</td>
                    <td>{x.time.substring(0,10)}</td>
                    <td>{x.place}</td>
                    <td>{x.roundPoints}</td>
                    <td>{x.score}</td>
                    <td className="d-none d-sm-table-cell">{x.hcp}</td>
                    <td className="d-none d-sm-table-cell">{x.hcpScore}</td>
                </tr>
                )}
            </tbody>
            </table>
        </div>
    );
  }

  render() {    
    let contents = this.state.loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : PlayerInfo.renderPlayerInfoTable(this.state.playerInfo);
    
    return (
      <div>
        <h1 id="tabelLabel">{this.state.name}</h1>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    var data = [];
    if(this.state.name !== "") {
        const response = await fetch(process.env.REACT_APP_API_ENDPOINT +'/api/Players/' + this.state.name);
        data = await response.json();
    }
    this.setState({ playerInfo: data, loading: false });
  }
}