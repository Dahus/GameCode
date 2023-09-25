using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GlobalData;

public class GeneralTreeTechnologies : AbstractTechnologyTree
{

    public override void SetupTechnologies(TechnologyInfo[] info)
    {
        _technologies = new List<AbstractTechnology>();
        // Объекты технологий называются так, как расположены на диаграмме в дизайн документе. (Ветка генерал)
        GeneralTechnologyLeft1 generalTechnologyLeft1 = new GeneralTechnologyLeft1();
        generalTechnologyLeft1.SetupTechnology(info[0].name, info[0].countOfMovesToStudy, info[0].resource, info[0].predecessorName1, info[0].predecessorName2, this);
        _technologies.Add(generalTechnologyLeft1);

        GeneralTechnologyRight1 generalTechnologyRight1 = new GeneralTechnologyRight1();
        generalTechnologyRight1.SetupTechnology(info[1].name, info[1].countOfMovesToStudy, info[1].resource, info[1].predecessorName1, info[1].predecessorName2, this);
        _technologies.Add(generalTechnologyRight1);

        GeneralTechnologyLeft2 generalTechnologyLeft2 = new GeneralTechnologyLeft2();
        generalTechnologyLeft2.SetupTechnology(info[2].name, info[2].countOfMovesToStudy, info[2].resource, info[2].predecessorName1, info[2].predecessorName2, this);
        _technologies.Add(generalTechnologyLeft2);

        GeneralTechnologyRight2 generalTechnologyRight2 = new GeneralTechnologyRight2();
        generalTechnologyRight2.SetupTechnology(info[3].name, info[3].countOfMovesToStudy, info[3].resource, info[3].predecessorName1, info[3].predecessorName2, this);
        _technologies.Add(generalTechnologyRight2);

        GeneralTechnologyCenter2 generalTechnologyCenter2 = new GeneralTechnologyCenter2();
        generalTechnologyCenter2.SetupTechnology(info[4].name, info[4].countOfMovesToStudy, info[4].resource, info[4].predecessorName1, info[4].predecessorName2, this);
        _technologies.Add(generalTechnologyCenter2);

        GeneralTechnologyRight3 generalTechnologyRight3 = new GeneralTechnologyRight3();
        generalTechnologyRight3.SetupTechnology(info[5].name, info[5].countOfMovesToStudy, info[5].resource, info[5].predecessorName1, info[5].predecessorName2, this);
        _technologies.Add(generalTechnologyRight3);

        GeneralTechnologyCenter3 generalTechnologyCenter3 = new GeneralTechnologyCenter3();
        generalTechnologyCenter3.SetupTechnology(info[6].name, info[6].countOfMovesToStudy, info[6].resource, info[6].predecessorName1, info[6].predecessorName2, this);
        _technologies.Add(generalTechnologyCenter3);

        GeneralTechnologyLeft3 generalTechnologyLeft3 = new GeneralTechnologyLeft3();
        generalTechnologyLeft3.SetupTechnology(info[7].name, info[7].countOfMovesToStudy, info[7].resource, info[7].predecessorName1, info[7].predecessorName2, this);
        _technologies.Add(generalTechnologyLeft3);

        GeneralTechnologyRight4 generalTechnologyRight4 = new GeneralTechnologyRight4();
        generalTechnologyRight4.SetupTechnology(info[8].name, info[8].countOfMovesToStudy, info[8].resource, info[8].predecessorName1, info[8].predecessorName2, this);
        _technologies.Add(generalTechnologyRight4);

        GeneralTechnologyCenter4 generalTechnologyCenter4 = new GeneralTechnologyCenter4();
        generalTechnologyCenter4.SetupTechnology(info[9].name, info[9].countOfMovesToStudy, info[9].resource, info[9].predecessorName1, info[9].predecessorName2, this);
        _technologies.Add(generalTechnologyCenter4);

        GeneralTechnologyLeft5 generalTechnologyLeft5 = new GeneralTechnologyLeft5();
        generalTechnologyLeft5.SetupTechnology(info[10].name, info[10].countOfMovesToStudy, info[10].resource, info[10].predecessorName1, info[10].predecessorName2, this);
        _technologies.Add(generalTechnologyLeft5);

        GeneralTechnologyCenter5 generalTechnologyCenter5 = new GeneralTechnologyCenter5();
        generalTechnologyCenter5.SetupTechnology(info[11].name, info[11].countOfMovesToStudy, info[11].resource, info[11].predecessorName1, info[11].predecessorName2, this);
        _technologies.Add(generalTechnologyCenter5);

        GeneralTechnologyCenter6 generalTechnologyCenter6 = new GeneralTechnologyCenter6();
        generalTechnologyCenter6.SetupTechnology(info[12].name, info[12].countOfMovesToStudy, info[12].resource, info[12].predecessorName1, info[12].predecessorName2, this);
        _technologies.Add(generalTechnologyCenter6);

        GeneralTechnologyRight6 generalTechnologyRight6 = new GeneralTechnologyRight6();
        generalTechnologyRight6.SetupTechnology(info[13].name, info[13].countOfMovesToStudy, info[13].resource, info[13].predecessorName1, info[13].predecessorName2, this);
        _technologies.Add(generalTechnologyRight6);
    }

    public List<AbstractTechnology> GetGeneralTreeTechnologies()
    {
        return _technologies;
    }
}
