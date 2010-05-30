﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SYNANLib;
using Operation_Structures_of_Texts.Classes.Text_Model;
using Operation_Structures_of_Texts.Classes.Sci_fi.Statistics;

namespace Operation_Structures_of_Texts.Classes.Sci_fi.Processors.Semantics.Rules.UnionsAndOther
{
    class GENIT_IG:IRule
    {
        #region IRule Members

        public WordRuleProbability check(ClausesTree clausesTree, int i, ISentence sent, StatisticsCollector stats, IOperationStructure os)
        {
            ElementaryProcess ep = (os as ElementaryProcess);
            if (clausesTree.rels[i].Name == "ГЕНИТ_ИГ")
            {
                if (!stats.isMarkedWord(clausesTree.rels[i].SourceItemNo))
                {
                    stats.addLog("Источник числительного не отмечен атрибутом операторной структуры (ГЕНИТ_ИГ)");
                    return null;
                }
                else
                {
                    LongOperationAttribut attr = null;

                    switch (stats.getTypeOfMarked(clausesTree.rels[i].SourceItemNo))
                    {
                        case "Action": attr = (ep.action as LongOperationAttribut); break;
                        case "Actor": attr = (ep.actor as LongOperationAttribut); break;
                        case "OFA": attr = (ep.objectForAction as LongOperationAttribut); break;
                        case "CFA": attr = (ep.charsOfAction as LongOperationAttribut); break;
                        case "AOFA": attr = (ep.additionalObjectsForAction as LongOperationAttribut); break;
                    }
                    attr.addElementaryAttribut(
                        sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                        "",
                        sent.get_Word(clausesTree.rels[i].TargetItemNo),
                        "UNION", clausesTree.rels[i].SourceItemNo,
                        sent,
                        attr
                        );
                    stats.markWord(clausesTree.rels[i].SourceItemNo, stats.getTypeOfMarked(clausesTree.rels[i].SourceItemNo),
                        i, SourceTargetEnum.Target);
                    return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                    sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                    new GENIT_IG(),
                    1,
                    i);
                }
            }
            return new WordRuleProbability(clausesTree.rels[i].TargetItemNo,
                    sent.get_Word(clausesTree.rels[i].TargetItemNo).WordStr,
                    new GENIT_IG(),
                    0,
                    i);
        }

        #endregion
    }
}