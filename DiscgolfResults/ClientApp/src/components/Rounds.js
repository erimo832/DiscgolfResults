import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import i18n from "../i18n";
import { Grid } from './common/Grid';

export class Rounds extends Component {
  static displayName = Rounds.name;

  constructor(props) {
    super(props);
    this.state = { rounds: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  renderRoundsTable(rounds) {
    return (<div>           
      <Collapse
        accordion={false}
      >
        {this.getItems(rounds)}
      </Collapse>
    </div>
    );
  }

  getItems(rounds) {
    const items = [];
    for (let i = 0, len = rounds.length; i < len; i++) {      
      items.push(
        <Panel header={`${rounds[i].eventName}`} key={rounds[i].eventId}>          
          <Grid data={this.getDataForGrid(rounds[i].results)} format={this.getGridConf()} />
        </Panel>
      );
    }
    return items;
  }

  getGridConf()
  {
    return {
      className: "table table-striped",
      key: "fullName",
      detailsArray: "",
      detailsValue: "",
      columns: [
        {columnName: "placementHcp",  headerText: i18n.t('column_place'),     headerClassName: "", rowClassName: ""},
        {columnName: "fullName",      headerText: i18n.t('column_name'),      headerClassName: "", rowClassName: ""},
        {columnName: "points",        headerText: i18n.t('column_points'),    headerClassName: "", rowClassName: ""},
        {columnName: "score",         headerText: i18n.t('column_score'),     headerClassName: "d-none d-sm-table-cell", rowClassName: "d-none d-sm-table-cell"},
        {columnName: "hcpBefore",     headerText: i18n.t('column_hcp'),       headerClassName: "d-none d-sm-table-cell", rowClassName: "d-none d-sm-table-cell"},
        {columnName: "hcpAfter",      headerText: i18n.t('column_hcpafter'),  headerClassName: "d-none d-md-table-cell", rowClassName: "d-none d-md-table-cell"},
        {columnName: "hcpScore",      headerText: i18n.t('column_hcpscore'),  headerClassName: "d-none d-sm-table-cell", rowClassName: "d-none d-sm-table-cell"},
        {columnName: "numberOfCtps",  headerText: i18n.t('column_ctp'),       headerClassName: "d-none d-md-table-cell", rowClassName: "d-none d-md-table-cell"}        
      ]
    };
  }

  getDataForGrid(data)
  {
    data.forEach(x => 
      { // add aggragated values
        x.isCtp = x.ctp === true ? "1": "0";        
      });

    return data;
  }

  render() {
    let contents = this.state.loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : this.renderRoundsTable(this.state.rounds);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('rounds_header')}</h1>
        <p>{i18n.t('rounds_description')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch('api/series/events');
    const data = await response.json();
    this.setState({ rounds: data, loading: false });
  }
}
