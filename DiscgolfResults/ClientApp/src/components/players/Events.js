import { useTranslation } from 'react-i18next';
import { Grid } from '../common/Grid';

export function Events(props) {
    const { i18n } = useTranslation();

    function getGridConf()
    {
      return {
        className: "table table-striped",
        key: "eventId",
        detailsArray: "",
        detailsValue: "",
        rowClass: function(data) { 
          return data.inHcpAvgCalc ? 'avg' : (data.inHcpCalc === true ? 'top' : 'none' ); 
        },
        columns: [
          {columnName: "eventName",   headerText: i18n.t('column_round'),    headerClassName: "", columnClass: ""},
          {columnName: "startTime",   headerText: i18n.t('column_date'),     headerClassName: "", columnClass: "", formatedValue: function(x) { return x.startTime.substring(0,10); }},
          {columnName: "placementHcp",headerText: i18n.t('column_place'),    headerClassName: "", columnClass: ""},
          {columnName: "points",      headerText: i18n.t('column_points'),   headerClassName: "", columnClass: ""},
          {columnName: "score",       headerText: i18n.t('column_score'),    headerClassName: "", columnClass: ""},
          {columnName: "hcpBefore",   headerText: i18n.t('column_hcp'),      headerClassName: "d-none d-sm-table-cell", columnClass: "d-none d-sm-table-cell"},
          {columnName: "hcpScore",    headerText: i18n.t('column_hcpscore'), headerClassName: "d-none d-sm-table-cell", columnClass: "d-none d-sm-table-cell"}       
        ]
      };
    }

    return (
        <>
            <Grid data={props.data} format={getGridConf()} />
        </>
      );
}