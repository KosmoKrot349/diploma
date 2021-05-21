using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.LabelMousDown
{
    class LabelClickFromGroopsSchedule:IMousDown
    {
        ManagerWindow window;
        object sender;

        public LabelClickFromGroopsSchedule(ManagerWindow window, object sender)
        {
            this.window = window;
            this.sender = sender;
        }

        public void MousDown()
        {
            Label label = sender as Label;
            window.iCoordScheduleLabel = Convert.ToInt32(label.Name.Split('_')[1]);
            window.jCoordScheduleLabel = Convert.ToInt32(label.Name.Split('_')[2]);
            for (int i = 0; i < (window.quanLessonsInDay * 7) + 1; i++)
            {
                for (int j = 1; j < window.quanGroops + 2; j++)
                {
                    if (i != 0 && j != 1)
                        window.labelArr[i, j].Background = Brushes.White;
                }
            }
            window.labelArr[window.iCoordScheduleLabel, window.jCoordScheduleLabel].Background = Brushes.Aqua;
            window.DeleteScheduleGroop.IsEnabled = false;
            window.GoToChangeScheduleGroop.IsEnabled = false;
            window.GoToAddScheduleGroop.IsEnabled = false;
            window.DeleteScheduleCabinet.IsEnabled = false;
            window.GoToChangeScheduleCabinet.IsEnabled = false;
            window.GoToAddScheduleCabinet.IsEnabled = false;
            window.DeleteScheduleTeacher.IsEnabled = false;
            window.GoToChangeScheduleTeacher.IsEnabled = false;
            window.GoToAddScheduleTeacher.IsEnabled = false;
            if (window.labelArr[window.iCoordScheduleLabel, window.jCoordScheduleLabel].Content.ToString() == "") { window.GoToAddScheduleGroop.IsEnabled = true; window.GoToAddScheduleCabinet.IsEnabled = true; window.GoToAddScheduleTeacher.IsEnabled = true; }
            if (window.labelArr[window.iCoordScheduleLabel, window.jCoordScheduleLabel].Content.ToString() != "")
            {
                window.DeleteScheduleGroop.IsEnabled = true;
                window.GoToChangeScheduleGroop.IsEnabled = true;

                window.DeleteScheduleCabinet.IsEnabled = true;
                window.GoToChangeScheduleCabinet.IsEnabled = true;

                window.DeleteScheduleTeacher.IsEnabled = true;
                window.GoToChangeScheduleTeacher.IsEnabled = true;
            }
        }
    }
}
