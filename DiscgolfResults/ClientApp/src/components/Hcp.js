import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import i18n from "../i18n";
import { Grid } from './common/Grid';

export class Hcp extends Component {
  static displayName = Hcp.name;

  constructor(props) {
    super(props);
    this.state = { hcp: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  renderHcpTable(hcpList) {
    return (
      <Grid data={this.getDataForGrid(hcpList)} format={this.getGridConf()} />
    );
  }

  getGridConf()
  {
    return {
      className: "table table-striped",
      key: "fullName",
      detailsArray: "",
      detailsValue: "",
      columns: [        
        {columnName: "fullName",  headerText: i18n.t('column_name'), headerClassName: "", rowClassName: ""},
        {columnName: "hcp",       headerText: i18n.t('column_hcp'),  headerClassName: "", rowClassName: ""}
      ]
    };
  }

  getDataForGrid(data)
  {
    return data;
  }

  render() {    
    let contents = this.state.loading
      ? <p><em>{i18n.t('common_loading')}</em></p>
      : this.renderHcpTable(this.state.hcp);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('hcp_header')}</h1>
        <p>{i18n.t('hcp_description_avgscore')}</p>
        <p>{i18n.t('hcp_description_hcp')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch(process.env.REACT_APP_API_ENDPOINT +'/api/Players/currentHcp');
    const data = await response.json();
    this.setState({ hcp: data, loading: false });
  }
}