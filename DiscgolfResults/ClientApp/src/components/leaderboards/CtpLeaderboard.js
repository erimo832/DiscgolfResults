import 'rc-collapse/assets/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from 'react';
import Collapse, { Panel } from 'rc-collapse';
import {Container, Row, Col } from 'react-bootstrap'
import i18n from "../../i18n";
import { Grid } from '../common/Grid';

export class CtpLeaderboard extends Component {
  static displayName = CtpLeaderboard.name;

  constructor(props) {
    super(props);
    this.state = { series: [], loading: true };
  }

  componentDidMount() {
    this.populateResultData();
  }

  renderSeriesTable(series) {
    return (<div>           
      <Collapse
        accordion={false}
      >
        {this.getItems(series)}
      </Collapse>
    </div>
    );
  }

  getItems(series) {
    const items = [];
    for (let i = 0, len = series.length; i < len; i++) {
      const key = i + 1;
      items.push(
        <Panel header={`${series[i].serieName}`} key={key}>          
          <Container>            
            <Row>            
              <Col sm={12} lg={12}>                
                <Grid data={this.getDataForGrid(series[i].placements)} format={this.getGridConf()} />
              </Col>              
            </Row>
            
        </Container>
        </Panel>
      );
    }
    return items;
  }

  getGridConf()
  {
    return {
      className: "table",
      key: "fullName",
      detailsArray: "",
      detailsValue: "",
      columns: [
        {columnName: "place",           headerText: i18n.t('column_place'),         headerClassName: "", rowClassName: ""},
        {columnName: "fullName",        headerText: i18n.t('column_name'),          headerClassName: "", rowClassName: ""},
        {columnName: "ctps",            headerText: i18n.t('column_numerofctps'),   headerClassName: "", rowClassName: ""},      
        {columnName: "numberOfRounds",  headerText: i18n.t('column_rounds'),        headerClassName: "d-none d-sm-table-cell", rowClassName: "d-none d-sm-table-cell"}        
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
      : this.renderSeriesTable(this.state.series);
     
    return (
      <div>
        <h1 id="tabelLabel">{i18n.t('leaderboard_ctp_header')}</h1>
        <p>{i18n.t('series_description')}</p>
        {contents}
      </div>
    );
  }

  async populateResultData() {
    const response = await fetch(process.env.REACT_APP_API_ENDPOINT +'/api/Series/ctpLeaderbords');
    const data = await response.json();
    this.setState({ series: data, loading: false });
  }
}