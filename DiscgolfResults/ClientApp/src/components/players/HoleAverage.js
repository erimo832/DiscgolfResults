import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

export function HoleAverage(props) {
  const { i18n } = useTranslation();

  function getContent(data) {
    if(data.length === 0)
        return (<div>{i18n.t('playersinfo_no_found')}</div>);
    
    return (
      <> 
        <ResponsiveContainer width="100%" height="100%" minHeight={300}>
          <BarChart width={500} height={300} data={data} margin={{ top: 5, right: 5, left: 5, bottom: 5, }} >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="holeNumber" />
            <YAxis domain={['auto', 'auto']} />
            <Tooltip content={<HoleAvgTooltip />}  />
            <Legend />
            <Bar name={i18n.t('hole_stats_diffpar')} dataKey="diffToPar" fill="#8884d8" />
          </BarChart>
        </ResponsiveContainer>
        <ResponsiveContainer width="100%" height="100%" minHeight={300}>
          <BarChart width={500} height={300} data={data} margin={{ top: 5, right: 5, left: 5, bottom: 5, }} >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="holeNumber" />
            <YAxis domain={[2, 'auto']} />
            <Tooltip content={<HoleAvgTooltip />}  />
            <Legend />
            <Bar name={i18n.t('hole_stats_avgscore')} dataKey="averageScore" fill="#8884d8" />
            <Bar name={i18n.t('hole_stats_par')} dataKey="par" fill="#82ca9d" />
          </BarChart>
        </ResponsiveContainer>
      </>
    );
  }

  return (
    <>
      {getContent(props.data)}
    </>
  );
}

const HoleAvgTooltip = ({ active, payload, label }) => {
  const { i18n } = useTranslation();

  if (active && payload && payload.length) {
    let diff = payload[0].payload.averageScore - payload[0].payload.par;
    
    let diffLabel = diff < 0 ? i18n.t('hole_stats_diffpar') : i18n.t('hole_stats_diffpar');
    return (
      <div className="custom-tooltip">        
          <p>{`Hole: ${label}`}</p>
          <p>
            {diffLabel + ": " + payload[0].payload.diffToPar}<br/>
            {i18n.t('hole_stats_avgscore')}: {payload[0].payload.averageScore} ({i18n.t('hole_stats_par')}: {payload[0].payload.par})
            
          </p>        
      </div>
    );
  }

  return null;
};
