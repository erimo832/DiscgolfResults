import { useTranslation } from 'react-i18next';
import { XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer, LineChart, Line } from 'recharts';

export function HcpTrend(props) {
    const { i18n } = useTranslation();

    return (
        <ResponsiveContainer width="100%" height="100%" minHeight={300}>
          <LineChart width={500} height={300} data={props.data} margin={{ top: 5, right: 5, left: 5, bottom: 5, }}>
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="playedEvent" />
            <YAxis />
            <Tooltip content={<CustomTooltip />} />
            <Legend />
            <Line name={i18n.t('player_details_legend_hcp')} type="monotone" dataKey="hcpAfter" stroke="#8884d8" dot={false} />
          </LineChart>
        </ResponsiveContainer>
      );
}

const CustomTooltip = ({ active, payload, label }) => {
    if (active && payload && payload.length) {
      return (
        <div className="custom-tooltip">        
            <p>{`Hcp: ${payload[0].value}`}</p>
            <p>
              {payload[0].payload.eventName} <br/>
              {payload[0].payload.startTime.substring(0,10)}
            </p>        
        </div>
      );
    }
  
    return null;
  };