import { useTranslation } from 'react-i18next';
import { XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer, LineChart, Line } from 'recharts';

export function AggregatedScoreTrend(props) {
    const { i18n } = useTranslation();

    function getScoreTrend(data) {
        let limit = 5;
        let cnt = 0;
        let sum = 0;
        let result = [];
        let from = data[0].startTime;    
    
    
        for (let i = 0; i < data.length; i++) {
            sum += data[i].score;
            cnt++;
    
            if(cnt === limit)
            {
                result.push(
                  {
                    avgScore: sum / cnt,
                    numValues: cnt,
                    from: from,
                    to: data[i].startTime
                  }
                );
                cnt = 0;
                sum = 0;
                from = data[i].startTime;
            }
        }
    
        //Add last
        if(cnt > 0) {
          result.push(
            {
              avgScore: sum / cnt,
              numValues: cnt,
              from: from,
              to: data[data.length - 1].startTime
            }
          );
        }
    
        return (
          <ResponsiveContainer width="100%" height="100%" minHeight={300}>
            <LineChart width={500} height={300} data={result} margin={{ top: 50, right: 5, left: 5, bottom: 5, }} >
              <CartesianGrid strokeDasharray="3 3" />
              <XAxis dataKey="playedEvent" />
              <YAxis domain={['auto', 'auto']} />
              <Tooltip content={<CustomAggregatedTooltip />} />
              <Legend />
              <Line name={i18n.t('player_details_legend_avgscore')} type="monotone" dataKey="avgScore" stroke="#8884d8" dot={false} />
            </LineChart>
          </ResponsiveContainer>
        );
      }

      return (
        <>
            {getScoreTrend(props.data)}
        </>
      );
}

const CustomAggregatedTooltip = ({ active, payload, label }) => {
    const { i18n } = useTranslation();
  
    if (active && payload && payload.length) {
      return (
        <div className="custom-tooltip">        
            <p>{`${i18n.t('player_details_legend_avgscore')}: ${payload[0].value}`}</p>
            <p>
              {i18n.t('player_details_tooltip_numvalues')}: {payload[0].payload.numValues} <br/>
              {i18n.t('player_details_tooltip_between')}: {payload[0].payload.from.substring(0,10)} {i18n.t('player_details_tooltip_and')} {payload[0].payload.to.substring(0,10)}
            </p>        
        </div>
      );
    }
  
    return null;
  };
