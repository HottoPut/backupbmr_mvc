﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class QueryPacking
    {
        String query;
        public String getJob() {
            query = @"SELECT JOB_SYS_ID
                    ,CAST((OPERATE_PERCENT+CHECK_PERCENT) AS nvarchar)+'|'+ISNULL(CAST(JOB_APPR_PD_PK_STATUS AS nvarchar)+'|','|')+ISNULL(USER_APPR_PD_JOB_USER_NAME+'|','|')+ISNULL(JOB_APPR_PD_PK_DT,'') AS APPR_JOB_PD
                    ,CAST((OPERATE_PERCENT+CHECK_PERCENT) AS nvarchar)+'|'+ISNULL(CAST(JOB_APPR_QA_PK_STATUS AS nvarchar)+'|','|')+ISNULL(USER_APPR_QA_JOB_USER_NAME+'|','|')+ISNULL(JOB_APPR_QA_PK_DT,'') AS APPR_JOB_QA
					,JOB_REMARK,JOB_ITEM_CODE,JOB_ITEM_NAME,JOB_LOT,BMR_VERSION
					,JOB_ITEM_CODE+'|'+JOB_ITEM_NAME+'|'+cast(BMR_VERSION as nvarchar)+
					ISNULL('|'+CONVERT(varchar,NULLIF(JOB_START_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(JOB_START_DT,''), 108),'|')+
					ISNULL('|'+CONVERT(varchar,NULLIF(JOB_END_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(JOB_END_DT,''), 108),'|') as JOB_ITEM_NAME2
					,CONVERT(varchar,NULLIF(JOB_START_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(JOB_START_DT,''), 108) AS JOB_START_DT
					,CONVERT(varchar,NULLIF(JOB_END_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(JOB_END_DT,''), 108) AS JOB_END_DT
                    ,PARSENAME(CONVERT(varchar, CAST(JOB_BATCH_SIZE AS money), 1), 2)+' '+JOB_UOM as JOB_BATCH_SIZE
                    ,JOB_STATUS
                    ,SUBSTRING(PKR_CC_RUN_NO,0,LEN(PKR_CC_RUN_NO)) AS PKR_CC_RUN_NO
                    ,SUBSTRING(PKR_CC_EQUIPMENT_NO,0,LEN(PKR_CC_EQUIPMENT_NO)) AS PKR_CC_EQUIPMENT_NO
                    ,SUBSTRING(PKR_CC_EQUIPMENT_NAME,0,LEN(PKR_CC_EQUIPMENT_NAME)) AS PKR_CC_EQUIPMENT_NAME
                    ,SUBSTRING(PKR_STEP_NO,0,LEN(PKR_STEP_NO)) AS  PKR_STEP_NO
                    ,SUBSTRING(PKR_RUN_NO,0,LEN(PKR_RUN_NO)) AS  PKR_RUN_NO
                    ,SUBSTRING(PKR_STEP_DESC,0,LEN(PKR_STEP_DESC)) AS PKR_STEP_DESC
					,SUBSTRING(PKR_CC_GROUP_CLEAN_ID,0,LEN(PKR_CC_GROUP_CLEAN_ID)) AS PKR_CC_GROUP_CLEAN_ID
					,SUBSTRING(PKR_CC_GROUP_CHECK_ID,0,LEN(PKR_CC_GROUP_CHECK_ID)) AS PKR_CC_GROUP_CHECK_ID
					,SUBSTRING(PKR_GROUP_OPERATE_ID,0,LEN(PKR_GROUP_OPERATE_ID)) AS PKR_GROUP_OPERATE_ID
					,SUBSTRING(PKR_GROUP_CHECK_ID,0,LEN(PKR_GROUP_CHECK_ID)) AS PKR_GROUP_CHECK_ID
					,SUBSTRING(PKR_REQ_TEMP_YN,0,LEN(PKR_REQ_TEMP_YN)) as PKR_REQ_TEMP_YN
					,SUBSTRING(PKR_REQ_HUMUDITY_YN,0,LEN(PKR_REQ_HUMUDITY_YN)) as PKR_REQ_HUMUDITY_YN
					,SUBSTRING(PKR_REQ_PRESS_YN,0,LEN(PKR_REQ_PRESS_YN)) as PKR_REQ_PRESS_YN
                    ,SUBSTRING(PKR_REQ_CLEAN_YN,0,LEN(PKR_REQ_CLEAN_YN)) as PKR_REQ_CLEAN_YN
					,SUBSTRING(PKR_REQ_VACC_YN,0,LEN(PKR_REQ_VACC_YN)) PKR_REQ_VACC_YN
					,SUBSTRING(PKR_REQ_START_STOP_YN,0,LEN(PKR_REQ_START_STOP_YN)) PKR_REQ_START_STOP_YN
					,SUBSTRING(PKR_REQ_WEIGHT_YN,0,LEN(PKR_REQ_WEIGHT_YN)) PKR_REQ_WEIGHT_YN
					,SUBSTRING(PKR_REQ_WEIGHT_SAMPLE_YN,0,LEN(PKR_REQ_WEIGHT_SAMPLE_YN)) PKR_REQ_WEIGHT_SAMPLE_YN
					,SUBSTRING(USER_NAME_CC_CLEAN,0,LEN(USER_NAME_CC_CLEAN)) AS USER_NAME_CC_CLEAN
					,SUBSTRING(USER_NAME_CC_CLEAN_DT,0,LEN(USER_NAME_CC_CLEAN_DT)) AS  USER_NAME_CC_CLEAN_DT
					,SUBSTRING(USER_NAME_CC_CHECK,0,LEN(USER_NAME_CC_CHECK)) AS USER_NAME_CC_CHECK
					,SUBSTRING(USER_NAME_CC_CHECK_DT,0,LEN(USER_NAME_CC_CHECK_DT)) AS USER_NAME_CC_CHECK_DT
					,SUBSTRING(USER_NAME_PK_OPERATE,0,LEN(USER_NAME_PK_OPERATE)) AS USER_NAME_PK_OPERATE
					,SUBSTRING(USER_NAME_PK_OPERATE_DT,0,LEN(USER_NAME_PK_OPERATE_DT)) AS USER_NAME_PK_OPERATE_DT
					,SUBSTRING(USER_NAME_PK_CHECK,0,LEN(USER_NAME_PK_CHECK)) AS USER_NAME_PK_CHECK
					,SUBSTRING(USER_NAME_PK_CHECK_DT,0,LEN(USER_NAME_PK_CHECK_DT)) USER_NAME_PK_CHECK_DT
                    ,SUBSTRING(PKR_REQ_IMAGE_YN,0,LEN(PKR_REQ_IMAGE_YN)) AS PKR_REQ_IMAGE_YN
                    ,SUBSTRING(PKR_CC_REQ_IMAGE_YN,0,LEN(PKR_CC_REQ_IMAGE_YN)) as PKR_CC_REQ_IMAGE_YN
					,OPERATE_PERCENT,CHECK_PERCENT
                    FROM(
                    SELECT distinct JOB_SYS_ID,JOB_APPR_PD_PK_STATUS,JOB_APPR_QA_PK_STATUS,JOB_CR_DT
					,CONVERT(varchar,NULLIF(JOB_APPR_PD_PK_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(JOB_APPR_PD_PK_DT,''), 108)  AS  JOB_APPR_PD_PK_DT
					,USER_APPR_PD_JOB.USER_NAME AS USER_APPR_PD_JOB_USER_NAME
					,CONVERT(varchar,NULLIF(JOB_APPR_QA_PK_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(JOB_APPR_QA_PK_DT,''), 108)  AS  JOB_APPR_QA_PK_DT
					,USER_APPR_QA_JOB.USER_NAME AS USER_APPR_QA_JOB_USER_NAME
					,JOB_START_DT,BMR_VERSION,JOB_END_DT,JOB_REMARK,JOB_ITEM_CODE,JOB_ITEM_NAME,JOB_LOT,JOB_BATCH_SIZE,JOB_STATUS,JOB_UOM
                    ,STUFF((SELECT  ISNULL(cast(t2.PKR_REQ_CLEAN_PROCESS_YN as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_REQ_CLEAN_YN
					,STUFF((SELECT  ISNULL(cast(t2.PKR_REQ_TEMP_YN as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_REQ_TEMP_YN
					,STUFF((SELECT  ISNULL(cast(t2.PKR_REQ_HUMUDITY_YN as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_REQ_HUMUDITY_YN
					,STUFF((SELECT  ISNULL(cast(t2.PKR_REQ_PRESS_YN as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_REQ_PRESS_YN
					,STUFF((SELECT  ISNULL(cast(t2.PKR_REQ_VACC_YN as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_REQ_VACC_YN
					,STUFF((SELECT  ISNULL(cast(t2.PKR_REQ_START_STOP_YN as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_REQ_START_STOP_YN
					,STUFF((SELECT  ISNULL(cast(t2.PKR_REQ_WEIGHT_YN as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_REQ_WEIGHT_YN
					,STUFF((SELECT  ISNULL(cast(t2.PKR_REQ_WEIGHT_SAMPLE_YN as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_REQ_WEIGHT_SAMPLE_YN
                    ,STUFF((SELECT  ISNULL(cast(t2.PKR_REQ_IMAGE_YN as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_REQ_IMAGE_YN
                    ,STUFF((SELECT  cast(t2.PKR_CC_RUN_NO as nvarchar(max))+'|' from BMR_PK_RUN_JOB_CC t2 where JOB_H_SYS_ID = t2.PKR_CC_BMR_H_SYS_ID AND t2.PKR_CC_JOB_SYS_ID = JOB_SYS_ID order by t2.PKR_CC_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_CC_RUN_NO
                    ,STUFF((SELECT  cast(t2.PKR_CC_EQUIPMENT_NO as nvarchar(max))+'|' from BMR_PK_RUN_JOB_CC t2 where JOB_H_SYS_ID = t2.PKR_CC_BMR_H_SYS_ID AND t2.PKR_CC_JOB_SYS_ID = JOB_SYS_ID order by t2.PKR_CC_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,0,'') PKR_CC_EQUIPMENT_NO
                    ,STUFF((SELECT  cast(t2.PKR_CC_EQUIPMENT_NAME as nvarchar(max))+'|' from BMR_PK_RUN_JOB_CC t2 where JOB_H_SYS_ID = t2.PKR_CC_BMR_H_SYS_ID AND t2.PKR_CC_JOB_SYS_ID = JOB_SYS_ID order by t2.PKR_CC_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_CC_EQUIPMENT_NAME
                    ,STUFF((SELECT  cast(t2.PKR_CC_REQ_IMAGE_YN as nvarchar(max))+'|' from BMR_PK_RUN_JOB_CC t2 where JOB_H_SYS_ID = t2.PKR_CC_BMR_H_SYS_ID AND t2.PKR_CC_JOB_SYS_ID = JOB_SYS_ID order by t2.PKR_CC_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_CC_REQ_IMAGE_YN
                    ,STUFF((SELECT  cast(t2.PKR_STEP_NO as nvarchar(max))+'|' from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_STEP_NO
                    ,STUFF((SELECT  cast(t2.PKR_RUN_NO as nvarchar(max))+'|' from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_RUN_NO
                    ,STUFF((SELECT  cast(t2.PKR_STEP_DESC as nvarchar(max))+'|' from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND JOB_SYS_ID = t2.PKR_JOB_SYS_ID order by  t2.PKR_STEP_NO,PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_STEP_DESC
					,STUFF((SELECT  ISNULL(cast(t2.PKR_CC_GROUP_CLEAN_ID as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_CC t2 where JOB_H_SYS_ID = t2.PKR_CC_BMR_H_SYS_ID AND t2.PKR_CC_JOB_SYS_ID = JOB_SYS_ID order by t2.PKR_CC_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_CC_GROUP_CLEAN_ID
					,STUFF((SELECT  ISNULL(cast(t2.PKR_CC_GROUP_CHECK_ID as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_CC t2 where JOB_H_SYS_ID = t2.PKR_CC_BMR_H_SYS_ID AND t2.PKR_CC_JOB_SYS_ID = JOB_SYS_ID order by t2.PKR_CC_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_CC_GROUP_CHECK_ID
					,STUFF((SELECT  ISNULL(cast(t2.PKR_GROUP_OPERATE_ID as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND t2.PKR_JOB_SYS_ID = JOB_SYS_ID order by t2.PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_GROUP_OPERATE_ID
					,STUFF((SELECT  ISNULL(cast(t2.PKR_GROUP_CHECK_ID as nvarchar(max))+'|','|') from BMR_PK_RUN_JOB_PROCEDURE t2 where JOB_H_SYS_ID = t2.PKR_BMR_H_SYS_ID AND t2.PKR_JOB_SYS_ID = JOB_SYS_ID order by t2.PKR_RUN_NO FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') ,1,0,'') PKR_GROUP_CHECK_ID
					,STUFF((SELECT  ISNULL(USER_NAME+'|','|')
							from BMR_PK_RUN_JOB_CC 
							LEFT JOIN BMR_PK_RUN_JOB_CC_CHECK  ON PKR_CC_RUN_NO=PK_CC_RJCHK_RUN_NO and PKR_CC_JOB_SYS_ID = PK_CC_RJCHK_JOB_SYS_ID
							LEFT JOIN BMR_USER_CTRL ON PK_CC_RJCHK_CLEAN_BY_USR_ID = USER_SYS_ID
							WHERE JOB_H_SYS_ID = PKR_CC_BMR_H_SYS_ID AND JOB_SYS_ID = PKR_CC_JOB_SYS_ID
							FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,0,'') AS USER_NAME_CC_CLEAN
				 	,STUFF((SELECT  ISNULL(CONVERT(varchar,NULLIF(PK_CC_RJCHK_CLEAN_BY_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_CC_RJCHK_CLEAN_BY_DT,''), 108) +'|','|')
							from BMR_PK_RUN_JOB_CC 
							LEFT JOIN BMR_PK_RUN_JOB_CC_CHECK  ON PKR_CC_RUN_NO=PK_CC_RJCHK_RUN_NO and PKR_CC_JOB_SYS_ID = PK_CC_RJCHK_JOB_SYS_ID
							LEFT JOIN BMR_USER_CTRL ON PK_CC_RJCHK_CLEAN_BY_USR_ID = USER_SYS_ID
							WHERE JOB_H_SYS_ID = PKR_CC_BMR_H_SYS_ID AND JOB_SYS_ID = PKR_CC_JOB_SYS_ID
							FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,0,'') AS USER_NAME_CC_CLEAN_DT
					,STUFF((SELECT  ISNULL(USER_NAME+'|','|')
							from BMR_PK_RUN_JOB_CC 
							LEFT JOIN BMR_PK_RUN_JOB_CC_CHECK  ON PKR_CC_RUN_NO=PK_CC_RJCHK_RUN_NO and PKR_CC_JOB_SYS_ID = PK_CC_RJCHK_JOB_SYS_ID
							LEFT JOIN BMR_USER_CTRL ON PK_CC_RJCHK_CHECK_BY_USR_ID = USER_SYS_ID
							WHERE JOB_H_SYS_ID = PKR_CC_BMR_H_SYS_ID AND JOB_SYS_ID = PKR_CC_JOB_SYS_ID
							FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,0,'') AS USER_NAME_CC_CHECK
					,STUFF((SELECT  ISNULL(CONVERT(varchar,NULLIF(PK_CC_RJCHK_CHECK_BY_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_CC_RJCHK_CHECK_BY_DT,''), 108) +'|','|')
							from BMR_PK_RUN_JOB_CC 
							LEFT JOIN BMR_PK_RUN_JOB_CC_CHECK  ON PKR_CC_RUN_NO=PK_CC_RJCHK_RUN_NO and PKR_CC_JOB_SYS_ID = PK_CC_RJCHK_JOB_SYS_ID
							LEFT JOIN BMR_USER_CTRL ON PK_CC_RJCHK_CHECK_BY_USR_ID = USER_SYS_ID
							WHERE JOB_H_SYS_ID = PKR_CC_BMR_H_SYS_ID AND JOB_SYS_ID = PKR_CC_JOB_SYS_ID
							FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,0,'') AS USER_NAME_CC_CHECK_DT
					,STUFF((SELECT  ISNULL(USER_NAME+'|','|')
							from BMR_PK_RUN_JOB_PROCEDURE  
								LEFT JOIN BMR_PK_RUN_JOB_CHECK  ON PKR_RUN_NO=PK_RJCHK_RUN_NO AND PKR_STEP_NO = PK_RJCHK_STEP and PKR_JOB_SYS_ID = PK_RJCHK_JOB_SYS_ID
								LEFT JOIN BMR_USER_CTRL ON PK_RJCHK_OPERATE_BY_USR_ID = USER_SYS_ID
							WHERE JOB_H_SYS_ID = PKR_BMR_H_SYS_ID AND JOB_SYS_ID = PKR_JOB_SYS_ID
                            order by PKR_STEP_NO,PKR_RUN_NO
							FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,0,'') AS USER_NAME_PK_OPERATE
					,STUFF((SELECT  ISNULL(CONVERT(varchar,NULLIF(PK_RJCHK_OPERATE_BY_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJCHK_OPERATE_BY_DT,''), 108) +'|','|')
							from BMR_PK_RUN_JOB_PROCEDURE
								LEFT JOIN BMR_PK_RUN_JOB_CHECK  ON PKR_RUN_NO=PK_RJCHK_RUN_NO AND PKR_STEP_NO = PK_RJCHK_STEP and PKR_JOB_SYS_ID = PK_RJCHK_JOB_SYS_ID
								LEFT JOIN BMR_USER_CTRL ON PK_RJCHK_OPERATE_BY_USR_ID = USER_SYS_ID
							WHERE JOB_H_SYS_ID = PKR_BMR_H_SYS_ID AND JOB_SYS_ID = PKR_JOB_SYS_ID
                            order by PKR_STEP_NO,PKR_RUN_NO
							FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,0,'') AS USER_NAME_PK_OPERATE_DT
					,STUFF((SELECT  ISNULL(USER_NAME+'|','|')
							from BMR_PK_RUN_JOB_PROCEDURE  
								LEFT JOIN BMR_PK_RUN_JOB_CHECK  ON PKR_RUN_NO=PK_RJCHK_RUN_NO AND PKR_STEP_NO = PK_RJCHK_STEP and PKR_JOB_SYS_ID = PK_RJCHK_JOB_SYS_ID
								LEFT JOIN BMR_USER_CTRL ON PK_RJCHK_CHECK_BY_USR_ID = USER_SYS_ID
							WHERE JOB_H_SYS_ID = PKR_BMR_H_SYS_ID AND JOB_SYS_ID = PKR_JOB_SYS_ID
                            order by PKR_STEP_NO,PKR_RUN_NO
							FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,0,'') AS USER_NAME_PK_CHECK
					,STUFF((SELECT  ISNULL(CONVERT(varchar,NULLIF(PK_RJCHK_CHECK_BY_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJCHK_CHECK_BY_DT,''), 108) +'|','|')
							from BMR_PK_RUN_JOB_PROCEDURE  
								LEFT JOIN BMR_PK_RUN_JOB_CHECK  ON PKR_RUN_NO=PK_RJCHK_RUN_NO AND PKR_STEP_NO = PK_RJCHK_STEP and PKR_JOB_SYS_ID = PK_RJCHK_JOB_SYS_ID
								LEFT JOIN BMR_USER_CTRL ON PK_RJCHK_CHECK_BY_USR_ID = USER_SYS_ID
							WHERE JOB_H_SYS_ID = PKR_BMR_H_SYS_ID AND JOB_SYS_ID = PKR_JOB_SYS_ID
                            order by PKR_STEP_NO,PKR_RUN_NO
							FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,0,'') AS USER_NAME_PK_CHECK_DT
                    FROM SHOW_PK_JOB
					LEFT JOIN  BMR_RUN_JOB  ON JOB_SYS_ID = APPR_PK_JOB_SYS_ID 
                    LEFT JOIN BMR_HEAD ON JOB_H_SYS_ID = BMR_H_SYS_ID
					LEFT JOIN BMR_USER_CTRL USER_APPR_PD_JOB ON JOB_APPR_PD_PK_USR_ID=USER_APPR_PD_JOB.USER_SYS_ID
					LEFT JOIN BMR_USER_CTRL USER_APPR_QA_JOB ON JOB_APPR_QA_PK_USR_ID=USER_APPR_QA_JOB.USER_SYS_ID)DATA1
					 LEFT JOIN  (
					SELECT JOB_SYS_ID AS JOB_SYS_ID_2
							,CASE WHEN SUM(COUNT_)<=0 THEN 0 ELSE CAST((CAST(SUM(OPERATE) AS float)/CAST(SUM(COUNT_) AS float))*100 AS decimal(18,2)) END as OPERATE_PERCENT
							,CASE WHEN SUM(COUNT_)<=0 THEN 0 ELSE CAST((CAST(SUM(CHECK_) AS float)/CAST(SUM(COUNT_) AS float))*100 AS decimal(18,2)) END as CHECK_PERCENT
							FROM(
									SELECT JOB_SYS_ID,COUNT(PK_CC_RJCHK_CLEAN_BY_USR_ID) AS OPERATE,COUNT(PK_CC_RJCHK_CHECK_BY_USR_ID) AS CHECK_
									,COUNT(PKR_CC_SYS_ID) AS COUNT_
									FROM  BMR_RUN_JOB
									LEFT JOIN BMR_HEAD ON  BMR_H_SYS_ID = JOB_H_SYS_ID
									LEFT JOIN BMR_PK_RUN_JOB_CC ON PKR_CC_BMR_H_SYS_ID = BMR_H_SYS_ID AND JOB_SYS_ID = PKR_CC_JOB_SYS_ID
									LEFT JOIN BMR_PK_RUN_JOB_CC_CHECK ON PK_CC_RJCHK_JOB_SYS_ID = JOB_SYS_ID AND PKR_CC_RUN_NO = PK_CC_RJCHK_RUN_NO
                                    WHERE PKR_CC_RUN_NO >0
									GROUP BY JOB_SYS_ID
									UNION
									SELECT JOB_SYS_ID,COUNT(PK_RJCHK_OPERATE_BY_USR_ID) AS OPERATE,COUNT(PK_RJCHK_CHECK_BY_USR_ID) AS CHECK_
									,COUNT(PKR_SYS_ID) AS COUNT_
									FROM  BMR_RUN_JOB
									LEFT JOIN BMR_HEAD ON  BMR_H_SYS_ID = JOB_H_SYS_ID
									LEFT JOIN BMR_PK_RUN_JOB_PROCEDURE ON PKR_BMR_H_SYS_ID = BMR_H_SYS_ID AND JOB_SYS_ID = PKR_JOB_SYS_ID
									LEFT JOIN BMR_PK_RUN_JOB_CHECK ON PK_RJCHK_JOB_SYS_ID = JOB_SYS_ID AND PK_RJCHK_STEP = PKR_STEP_NO AND PKR_RUN_NO = PK_RJCHK_RUN_NO
                                    WHERE PKR_RUN_NO > 0
									GROUP BY JOB_SYS_ID
							)DATA1
							GROUP BY JOB_SYS_ID
					)DATA2 ON DATA2.JOB_SYS_ID_2 = DATA1.JOB_SYS_ID
                    order by JOB_CR_DT desc";
            return query;
        }
        public String QueryGetTime()
        {
            query = @"SELECT CONVERT(varchar,NULLIF(PK_RJDT_FN_MX_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJDT_FN_MX_DT,''), 108) as PK_RJDT_FN_MX_DT
                        ,CONVERT(varchar,NULLIF(PK_RJDT_ST_FIL_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJDT_ST_FIL_DT,''), 108) as PK_RJDT_ST_FIL_DT
                        ,CONVERT(varchar,NULLIF(PK_RJDT_FN_FIL_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJDT_FN_FIL_DT,''), 108) as PK_RJDT_FN_FIL_DT
                        ,CONVERT(varchar,NULLIF(PK_RJDT_FN_LABEL_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJDT_FN_LABEL_DT,''), 108) as PK_RJDT_FN_LABEL_DT
                        ,CONVERT(varchar,NULLIF(PK_RJDT_FN_CARTON_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJDT_FN_CARTON_DT,''), 108) as PK_RJDT_FN_CARTON_DT 
                        FROM BMR_PK_RUN_JOB_TIME
                      WHERE PK_RJDT_JOB_SYS_ID = @P_JOB_SYS_ID AND PK_RJDT_STEP_NO = @P_STEP_NO AND PK_RJDT_RUN_NO = @P_RUN_NO";
            return query;
        }
        public String InsertTemp()
        {
            query = "EXEC INSERT_BMR_PK_RUN_JOB_TEMP @JOB_SYS_ID = @P_JOB_SYS_ID,@STEP = @P_STEP,@RUN_NO = @P_RUN_NO,@TEMP = @P_TEMP,@USER_ID = @P_USER_ID";
            return query;
        }
        public String QueryRunJobGetTemp()
        {
            //Scalar
            query = "SELECT PK_RJT_TEMP FROM BMR_PK_RUN_JOB_TEMP WHERE PK_RJT_JOB_SYS_ID  = @P_JOB_SYS_ID AND PK_RJT_STEP_NO = @P_STEP AND PK_RJT_RUN_NO= @P_RUN_NO";
            return query;
        }
        public String QueryRunJobGetPressure()
        {
            //Scalar
            query = "SELECT PK_RJP_PRESSURE FROM BMR_PK_RUN_JOB_PRESSURE WHERE PK_RJP_JOB_SYS_ID = @P_JOB_SYS_ID AND PK_RJP_STEP_NO = @P_STEP AND PK_RJP_RUN_NO = @P_RUN_NO";
            return query;
        }
        public String InsertPressure()
        {
            query = "EXEC INSERT_BMR_PK_RUN_JOB_PRESSURE @JOB_SYS_ID = @P_JOB_SYS_ID,@STEP = @P_STEP,@RUN_NO = @P_RUN_NO,@PRESSURE = @P_PRESSURE,@USER_ID = @P_USER_ID";
            return query;
        }
        public String InsertHumidity()
        {
            query = "EXEC INSERT_BMR_PK_RUN_JOB_HUMIDITY @JOB_SYS_ID = @P_JOB_SYS_ID,@STEP = @P_STEP,@RUN_NO = @P_RUN_NO,@HUMIDITY = @P_HUMIDITY,@USER_ID = @P_USER_ID";
            return query;
        }
        public String InsertTime()
        {
            query = "EXEC INSERT_BMR_PK_RUN_JOB_TIME @JOB_SYS_ID = @P_JOB_SYS_ID,@STEP = @P_STEP,@RUN_NO = @P_RUN_NO,@fnMxDt = @P_fnMxDt,@stFilDt = @P_stFilDt,@fnFilDt = @P_fnFilDt,@fnLabelDt = @P_fnLabelDt,@fnCartonDt = @P_fnCartonDt,@USER_ID = @P_USER_ID";
            return query;
        }
        public String QueryRunJobGetTime() {
            query = "";
            return query;
        }
        public String QueryRunJobGetHumidity()
        {
            //Scalar
            query = "SELECT PK_RJH_HUMIDITY FROM BMR_PK_RUN_JOB_HUMIDITY WHERE PK_RJH_JOB_SYS_ID = @P_JOB_SYS_ID AND PK_RJH_STEP_NO = @P_STEP AND PK_RJH_RUN_NO = @P_RUN_NO";
            return query;
        }
        public String CheckPkCCCleanChk()
        {
            query = "EXEC PK_CC_CLEAN_CHECK @USERID = @P_USERID,@RUN_NO = @P_RUN_NO,@JOB_SYS_ID = @P_JOB_SYS_ID,@USER_LOGIN = @P_USER_LOGIN,@USER_PASS = @P_USER_PASS";
            return query;
        }
        public String CheckPkCCChkChk()
        {
            query = "EXEC PK_CC_CHECK_CHECK @USERID = @P_USERID,@RUN_NO = @P_RUN_NO,@JOB_SYS_ID = @P_JOB_SYS_ID,@USER_LOGIN = @P_USER_LOGIN,@USER_PASS = @P_USER_PASS";
            return query;
        }
        public String CheckPkOperateChk()
        {
            query = "EXEC PK_OPERATE_CHECK @USERID = @P_USERID,@STEP = @P_STEP,@RUN_NO = @P_RUN_NO,@JOB_SYS_ID = @P_JOB_SYS_ID,@USER_LOGIN = @P_USER_LOGIN,@USER_PASS = @P_USER_PASS";
            return query;
        }
        public String CheckPkChkChk()
        {
            query = "EXEC PK_CHECK_CHECK @USERID = @P_USERID,@STEP = @P_STEP,@RUN_NO = @P_RUN_NO,@JOB_SYS_ID = @P_JOB_SYS_ID,@USER_LOGIN = @P_USER_LOGIN,@USER_PASS = @P_USER_PASS";
            return query;
        }
        public String QueryRunJobImage()
        {
            query = "SELECT PK_RJPT_SYS_ID,PK_RJPT_IMAGE_NAME,PK_RJPT_LIST_NO,PK_RJPT_IMAGE_PATH,PK_RJPT_REMARK FROM BMR_PK_RUN_JOB_PICTURE WHERE PK_RJPT_JOB_SYS_ID =@P_SYS_ID AND PK_RJPT_STEP_NO = @P_STEP AND PK_RJPT_RUN_NO =@P_RUNNO";
            return query;
        }
        public String InsertRunJobImage()
        {
            query = "EXEC INSERT_BMR_PK_RUN_JOB_IMAGE @JOB_SYS_ID = @P_JOB_SYS_ID,@STEP = @P_STEP,@RUN_NO = @P_RUN_NO,@LIST_NO = @P_LIST_NO,@LENGTH = @P_LENGTH,@IMAGE_NAME = @P_IMAGE_NAME,@IMAGE_PATH = @P_IMAGE_PATH,@IMAGE_REMARK = @P_REMARK,@USER_ID = @P_USERID";
            return query;
        }
        public String QueryCCRunJobImage()
        {
            query = @"SELECT PK_CC_RJPT_SYS_ID,PK_CC_RJPT_IMAGE_NAME,PK_CC_RJPT_LIST_NO,PK_CC_RJPT_IMAGE_PATH,PK_CC_RJPT_REMARK
                      FROM BMR_PK_RUN_JOB_CC_PICTURE
                      WHERE PK_CC_RJPT_JOB_SYS_ID =@P_SYS_ID  AND PK_CC_RJPT_RUN_NO =@P_RUNNO";
            return query;
        }
        public String CCInsertRunJobImage()
        {
            query = "EXEC INSERT_BMR_PK_RUN_JOB_CC_IMAGE @JOB_SYS_ID = @P_JOB_SYS_ID,@RUN_NO = @P_RUN_NO,@LIST_NO = @P_LIST_NO,@LENGTH = @P_LENGTH,@IMAGE_NAME = @P_IMAGE_NAME,@IMAGE_PATH = @P_IMAGE_PATH,@IMAGE_REMARK = @P_REMARK,@USER_ID = @P_USERID";
            return query;
        }
        public String CCUpdateRunJobImage()
        {
            //Remark
            query = "UPDATE BMR_PK_RUN_JOB_CC_PICTURE SET PK_CC_RJPT_REMARK = @P_REMARK,PK_CC_RJPT_UPD_USR_ID = @P_USER_ID,PK_CC_RJPT_UPD_DT = SYSDATETIME() WHERE PK_CC_RJPT_JOB_SYS_ID = @P_JOB_SYS_ID AND  PK_CC_RJPT_RUN_NO = @P_RUN_NO";
            return query;
        }
        public String ApprPdJob()
        {
            query = "EXEC APPR_PD_PK_RUN_JOB @JOB_SYS_ID = @P_JOB_SYS_ID,@USER_ID = @P_USER_ID ,@USER_LOGIN = @P_USER_LOGIN,@USER_PASS = @P_USER_PASS";
            return query;
        }
        public String ApprQaJob()
        {
            query = "EXEC APPR_QA_PK_RUN_JOB @JOB_SYS_ID = @P_JOB_SYS_ID,@USER_ID = @P_USER_ID ,@USER_LOGIN = @P_USER_LOGIN,@USER_PASS = @P_USER_PASS";
            return query;
        }
        public String InsertClean() {
            query = "EXEC INSERT_BMR_PK_RUN_JOB_CLEAN @JOB_SYS_ID = @P_JOB_SYS_ID,@STEP = @P_STEP,@RUN_NO = @P_RUN_NO,@AMOUNT_BOT = @P_AMOUNT_BOT,@WATER_TEMP = @P_WATER_TEMP,@BOT_START_DT = @P_BOT_START_DT,@BOT_END_DT = @P_BOT_END_DT,@AMOUNT_LID = @P_AMOUNT_LID,@LID_START_DT = @P_LID_START_DT,@LID_END_DT = @P_LID_END_DT,@AMOUNT_BAKE_LID = @P_AMOUNT_BAKE_LID,@ICBT_TEMP = @P_ICBT_TEMP,@BAKE_START_DT = @P_BAKE_START_DT,@BAKE_END_DT = @P_BAKE_END_DT,@USER_ID = @P_USER_ID";
            return query;
        }
        public String QueryGetClean() {
            query = @"select 
                    PK_RJC_AMOUNT_BOT,PK_RJC_WATER_TEMP
                    ,CONVERT(varchar,NULLIF(PK_RJC_BOT_START_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJC_BOT_START_DT,''), 108) AS PK_RJC_BOT_START_DT  
                    ,CONVERT(varchar,NULLIF(PK_RJC_BOT_END_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJC_BOT_END_DT,''), 108) AS PK_RJC_BOT_END_DT  
                    ,PK_RJC_AMOUNT_LID
                    ,CONVERT(varchar,NULLIF(PK_RJC_LID_START_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJC_LID_START_DT,''), 108) AS PK_RJC_LID_START_DT  
                    ,CONVERT(varchar,NULLIF(PK_RJC_LID_END_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJC_LID_END_DT,''), 108) AS PK_RJC_LID_END_DT  
                    ,PK_RJC_AMOUNT_BAKE_LID,PK_RJC_ICBT_TEMP
                    ,CONVERT(varchar,NULLIF(PK_RJC_BAKE_START_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJC_BAKE_START_DT,''), 108) AS PK_RJC_BAKE_START_DT
                    ,CONVERT(varchar,NULLIF(PK_RJC_BAKE_END_DT,''),103)+' '+CONVERT(VARCHAR(5), NULLIF(PK_RJC_BAKE_END_DT,''), 108) AS PK_RJC_BAKE_END_DT
                    from BMR_PK_RUN_JOB_CLEAN
                    where PK_RJC_JOB_SYS_ID = @P_JOB_SYS_ID and PK_RJC_STEP_NO = @P_STEP and PK_RJC_RUN_NO = @P_RUN_NO";
            return query;
        }
        public String InsertRunJobCC()
        {
            query = "EXEC INSERT_BMR_PK_RUN_JOB_CC @JOB_SYS_ID = @P_JOB_SYS_ID,@RUN_NO = @P_RUN_NO,@EQUIPMENT_NO = @P_EQUIPMENT_NO,@EQUIPMENT_NAME = @P_EQUIPMENT_NAME,@GROUP_CLEAN = @P_GROUP_CLEAN,@GROUP_CHECK = @P_GROUP_CHECK,@REQ_IMAGE_YN = @P_REQ_IMAGE_YN,@USER_ID = @P_USER_ID";
            return query;
        }
        public String InsertRunJobWeightOfSample()
        {
            query = "EXEC INSERT_BMR_PK_RUN_JOB_WEIGHT_SAMPLE @JOB_SYS_ID = @P_JOB_SYS_ID,@STEP = @P_STEP,@RUN_NO = @P_RUN_NO,@WEIGHT = @P_WEIGHT,@USER_ID = @P_USER_ID";
            return query;
        }
        public String QueryRunJobWeightOfSample()
        {
            //Scalar
            query = "SELECT PK_RJWS_WEIGHT FROM BMR_PK_RUN_JOB_WEIGHT_SAMPLE WHERE PK_RJWS_JOB_SYS_ID = @P_JOB_SYS_ID AND PK_RJWS_STEP_NO = @P_STEP AND PK_RJWS_RUN_NO = @P_RUN_NO";
            return query;
        }
    }
}