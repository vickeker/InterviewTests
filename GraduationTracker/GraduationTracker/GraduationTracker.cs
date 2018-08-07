using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {   
        public Tuple<bool, Standing>  HasGraduated(Diploma diploma, Student student)
        {
            var credits = 0;
            var average = 0;
        
            for(int i = 0; i < diploma.Requirements.Length; i++)
            {
                for(int j = 0; j < student.Courses.Length; j++)
                {
                    var requirement = Repository.GetRequirement(diploma.Requirements[i]);

                    for (int k = 0; k < requirement.Courses.Length; k++)
                    {
                        if (requirement.Courses[k] == student.Courses[j].Id)
                        {
                            average += student.Courses[j].Mark;
                            if (student.Courses[j].Mark > requirement.MinimumMark)
                            {
                                credits += requirement.Credits;     //Potential correction: credits are never used in this function
                            }
                        }
                    }
                }
            }

            average = average / student.Courses.Length;

            var standing = Standing.None;

            if (average < 50)
                standing = Standing.Remedial;
            else if (average < 80)
                standing = Standing.Average;
            else if (average < 95)
                standing = Standing.MagnaCumLaude;   // Potential correction: Could be change for SumaCumLaude
            else
                standing = Standing.MagnaCumLaude;

            switch (standing)
            {
                case Standing.Remedial:
                    return new Tuple<bool, Standing>(false, standing);
                case Standing.Average:
                    return new Tuple<bool, Standing>(true, standing);
                case Standing.SumaCumLaude:
                    return new Tuple<bool, Standing>(true, standing);
                case Standing.MagnaCumLaude:
                    return new Tuple<bool, Standing>(true, standing);

                default:
                    return new Tuple<bool, Standing>(false, standing);
            } 
        }
    }
}
