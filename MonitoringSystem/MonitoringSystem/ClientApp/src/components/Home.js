import React from 'react';
import { connect } from 'react-redux';
import Plot from 'react-plotly.js';

const Home = props => (
  <div>
     <Plot
        data={[{
          x: ['2013-10-04 22:00:00', '2013-10-04 22:02:00', '2013-10-04 22:04:00', '2013-10-04 22:06:00',
            '2013-10-04 22:08:00', '2013-10-04 22:10:00'
          ],
          y: [1, 3, 6, 2, 4, 5],
          type: 'scatter'
        },
        {
          x: ['2013-10-04 22:00:00', '2013-10-04 22:02:00', '2013-10-04 22:04:00', '2013-10-04 22:06:00',
            '2013-10-04 22:08:00', '2013-10-04 22:10:00'
          ],
          y: [10, 13, 16, 10, 8, 6],
          type: 'scatter'
        }
      ]}
        layout={ {title: 'Nhiệt Độ Cảm Biến'} }
      />
  </div>
);

export default connect()(Home);
