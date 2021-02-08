using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMR_MVC.Models
{
    public class QueryRunJob
    {
        String query;
        public String UpdateBmrHead() {
            query = "EXEC UPDATE_BMR_HEAD @ITEM_CODE = @P_ITEM_CODE,@RV1 = @P_RV1,@RV2 = @P_RV2,@RV_DATE =  @P_RV_DATE,@STATUS_BMR = @P_STATUS_BMR,@START_DT = @P_START_DT,@END_DT = @P_END_DT";
            return query;
        }
        public String QueryGetStatusBMR() {
            query = @"select BMR_ITEM_CODE,BMR_VERSION
                    ,case when BMR_START_DATE<=SYSDATETIME() and BMR_END_DATE>=SYSDATETIME()  then BMR_STATUS ELSE 0 end as BMR_STATUS
                    ,convert(varchar,BMR_START_DATE,103) as BMR_START_DATE
                    ,convert(varchar,BMR_END_DATE,103) as BMR_END_DATE
                    from bmr_head 
                    where BMR_ITEM_CODE = @P_ITEM_CODE 
                    and BMR_RV1 = @P_RV1
					and BMR_RV2 = @P_RV2
					and CONVERT(VARCHAR,BMR_RV_DATE,4) = @P_RV_DT";
            return query;
        }
        public String QueryGetPKCleanControl() {
            query = @"select PK_CC_BMR_H_SYS_ID,PK_CC_RUN_NO,PK_CC_EQUIPMENT_NO,PK_CC_EQUIPMENT_NAME,GROUP_OP.GROUP_NAME AS GROUP_OP_NAME
                        ,GROUP_CHECK.GROUP_NAME AS GROUP_CHECK_NAME,PK_CC_REQ_IMAGE_YN 
                        from BMR_HEAD LEFT JOIN BMR_PK_CLEAN_CONTROL ON PK_CC_BMR_H_SYS_ID = BMR_H_SYS_ID 
                        LEFT JOIN BMR_USER_GROUP GROUP_OP ON PK_CC_GROUP_CLEAN_ID=GROUP_OP.GROUP_ID
                        LEFT JOIN BMR_USER_GROUP GROUP_CHECK ON PK_CC_GROUP_CHECK_ID=GROUP_CHECK.GROUP_ID
                        WHERE BMR_ITEM_CODE= @P_ITEM_CODE 
                        and BMR_RV1 = @P_RV1
						and BMR_RV2 = @P_RV2
						and CONVERT(VARCHAR,BMR_RV_DATE,4) = @P_RV_DT";
            return query;
        }
        public String QueryGetPk() {
            //query = @"select PK_BMR_H_SYS_ID,PK_STEP_NO,PK_RUN_NO,PK_STEP_DESC from BMR_HEAD LEFT JOIN BMR_PK_PROCEDURE ON BMR_H_SYS_ID = PK_BMR_H_SYS_ID
            //          WHERE BMR_ITEM_CODE = @P_ITEM_CODE AND BMR_VERSION = @P_VERSION";
            query = @"select PK_BMR_H_SYS_ID,PK_STEP_NO,PK_STEP_DESC,BMR_VERSION
                        ,substring(SUB_RUN_NO,0,LEN(SUB_RUN_NO)) as SUB_RUN_NO,substring(SUB_DESC,0,LEN(SUB_DESC)) as SUB_DESC
                        ,substring(SUB_GROUP_OP,0,LEN(SUB_GROUP_OP)) as SUB_GROUP_OP,substring(SUB_GROUP_CHECK,0,LEN(SUB_GROUP_CHECK)) as SUB_GROUP_CHECK
                        ,substring(SUB_REQ_IMAGE_YN,0,LEN(SUB_REQ_IMAGE_YN)) as SUB_REQ_IMAGE_YN,substring(SUB_REQ_HUMIDITY_YN,0,LEN(SUB_REQ_HUMIDITY_YN)) as SUB_REQ_HUMIDITY_YN
                        ,substring(SUB_REQ_PRESS_YN,0,LEN(SUB_REQ_PRESS_YN)) as SUB_REQ_PRESS_YN,substring(SUB_REQ_TEMP_YN,0,LEN(SUB_REQ_TEMP_YN)) as SUB_REQ_TEMP_YN
                        ,substring(SUB_REQ_VACC_YN,0,LEN(SUB_REQ_VACC_YN)) as SUB_REQ_VACC_YN,substring(SUB_REQ_START_STOP_YN,0,LEN(SUB_REQ_START_STOP_YN)) as SUB_REQ_START_STOP_YN
                        ,substring(SUB_REQ_WEIGHT_YN,0,LEN(SUB_REQ_WEIGHT_YN)) as SUB_REQ_WEIGHT_YN,substring(SUB_REQ_WEIGHT_SAMPLE_YN,0,LEN(SUB_REQ_WEIGHT_SAMPLE_YN)) as SUB_REQ_WEIGHT_SAMPLE_YN
                        ,substring(SUB_REQ_CLEAN_PROCESS_YN,0,LEN(SUB_REQ_CLEAN_PROCESS_YN)) as SUB_REQ_CLEAN_PROCESS_YN
                        from(
                        select BMR_VERSION
                        ,STUFF((select CAST(TP_DESC.PK_STEP_NO as nvarchar)+'.'+cast(TP_DESC.PK_RUN_NO as nvarchar)+'|'
							from  BMR_PK_PROCEDURE TP_DESC 
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_RUN_NO
                        ,STUFF((select TP_DESC.PK_STEP_DESC+'|'
							from  BMR_PK_PROCEDURE TP_DESC 
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_DESC
                        ,STUFF((select ISNULL(TP_GROUP_OP.GROUP_NAME+'|','|')
							from  BMR_PK_PROCEDURE TP_DESC  
							LEFT JOIN  BMR_USER_GROUP TP_GROUP_OP  ON TP_DESC.PK_GROUP_OPERATE_ID=TP_GROUP_OP.GROUP_ID
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_GROUP_OP
                        ,STUFF((select ISNULL(TP_GROUP_OP.GROUP_NAME+'|','|')
							from  BMR_PK_PROCEDURE TP_DESC  
							LEFT JOIN  BMR_USER_GROUP TP_GROUP_OP  ON TP_DESC.PK_GROUP_CHECK_ID=TP_GROUP_OP.GROUP_ID
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_GROUP_CHECK
                        ,STUFF((select ISNULL(TP_DESC.PK_REQ_IMAGE_YN+'|','N|')
							from  BMR_PK_PROCEDURE TP_DESC 
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_IMAGE_YN
                        ,STUFF((select ISNULL(TP_DESC.PK_REQ_HUMIDITY_YN+'|','N|')
							from  BMR_PK_PROCEDURE TP_DESC 
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_HUMIDITY_YN
                        ,STUFF((select ISNULL(TP_DESC.PK_REQ_PRESS_YN+'|','N|')
							from  BMR_PK_PROCEDURE TP_DESC 
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_PRESS_YN
						,STUFF((select ISNULL(TP_DESC.PK_REQ_TEMP_YN+'|','N|')
							from  BMR_PK_PROCEDURE TP_DESC 
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_TEMP_YN
						,STUFF((select ISNULL(TP_DESC.PK_REQ_VACC_YN+'|','N|')
							from  BMR_PK_PROCEDURE TP_DESC 
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_VACC_YN
						,STUFF((select ISNULL(TP_DESC.PK_REQ_START_STOP_YN+'|','N|')
							from  BMR_PK_PROCEDURE TP_DESC 
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_START_STOP_YN
						,STUFF((select ISNULL(TP_DESC.PK_REQ_WEIGHT_YN+'|','N|')
							from  BMR_PK_PROCEDURE TP_DESC 
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_WEIGHT_YN
						,STUFF((select ISNULL(TP_DESC.PK_REQ_WEIGHT_SAMPLE_YN+'|','N|')
							from  BMR_PK_PROCEDURE TP_DESC 
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_WEIGHT_SAMPLE_YN
					    ,STUFF((select ISNULL(TP_DESC.PK_REQ_CLEAN_PROCESS_YN+'|','N|')
							from  BMR_PK_PROCEDURE TP_DESC 
							where TP_DESC.PK_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.PK_STEP_NO=TP_MAIN.PK_STEP_NO
							and TP_DESC.PK_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_CLEAN_PROCESS_YN
                        ,PK_BMR_H_SYS_ID,PK_STEP_NO,PK_RUN_NO,PK_STEP_DESC,GROUP_OP.GROUP_NAME AS GROUP_OP_NAME
                        ,GROUP_CHECK.GROUP_NAME AS GROUP_CHECK_NAME
                        ,PK_REQ_IMAGE_YN,PK_REQ_HUMIDITY_YN,PK_REQ_PRESS_YN,PK_REQ_TEMP_YN,PK_REQ_VACC_YN
                        ,PK_REQ_START_STOP_YN,PK_REQ_WEIGHT_YN,PK_REQ_WEIGHT_SAMPLE_YN,PK_STANDARD_W
                        from BMR_HEAD LEFT JOIN BMR_PK_PROCEDURE TP_MAIN ON BMR_H_SYS_ID = PK_BMR_H_SYS_ID
                        LEFT JOIN BMR_USER_GROUP GROUP_OP ON PK_GROUP_OPERATE_ID=GROUP_OP.GROUP_ID
                        LEFT JOIN BMR_USER_GROUP GROUP_CHECK ON PK_GROUP_OPERATE_ID=GROUP_CHECK.GROUP_ID
                        WHERE BMR_ITEM_CODE = @P_ITEM_CODE 
                        and BMR_RV1 = @P_RV1
						and BMR_RV2 = @P_RV2
						and CONVERT(VARCHAR,BMR_RV_DATE,4) = @P_RV_DT
                        and PK_RUN_NO=0)data1
                        ORDER BY PK_STEP_NO,PK_RUN_NO";
            return query;
        }
        public String QueryGetBcrCleanControl()
        {
            query = @"select BCR_CC_BMR_H_SYS_ID,BCR_CC_RUN_NO,BCR_CC_EQUIPMENT_NO,BCR_CC_EQUIPMENT_NAME from BMR_HEAD LEFT JOIN BMR_BCR_CLEAN_CONTROL ON BCR_CC_BMR_H_SYS_ID = BMR_H_SYS_ID
                        WHERE BMR_ITEM_CODE= @P_ITEM_CODE 
                        and BMR_RV1 = @P_RV1
						and BMR_RV2 = @P_RV2
						and CONVERT(VARCHAR,BMR_RV_DATE,4) = @P_RV_DT";
            return query;
        }
        public String QueryGerBcr()
        {
            //query = @"select BCR_BMR_H_SYS_ID,BCR_STEP_NO,BCR_RUN_NO,BCR_STEP_DESC from BMR_HEAD LEFT JOIN BMR_BCR_PROCEDURE ON BMR_H_SYS_ID = BCR_BMR_H_SYS_ID
            //          WHERE BMR_ITEM_CODE = @P_ITEM_CODE AND BMR_VERSION = @P_VERSION";
            query = @"select BCR_BMR_H_SYS_ID,BCR_STEP_NO,BCR_STEP_DESC,BMR_VERSION
                        ,substring(SUB_RUN_NO,0,LEN(SUB_RUN_NO)) as SUB_RUN_NO,substring(SUB_DESC,0,LEN(SUB_DESC)) as SUB_DESC
                        ,substring(SUB_GROUP_OP,0,LEN(SUB_GROUP_OP)) as SUB_GROUP_OP,substring(SUB_GROUP_CHECK,0,LEN(SUB_GROUP_CHECK)) as SUB_GROUP_CHECK
                        ,substring(SUB_REQ_IMAGE_YN,0,LEN(SUB_REQ_IMAGE_YN)) as SUB_REQ_IMAGE_YN,substring(SUB_REQ_HUMIDITY_YN,0,LEN(SUB_REQ_HUMIDITY_YN)) as SUB_REQ_HUMIDITY_YN
                        ,substring(SUB_REQ_PRESS_YN,0,LEN(SUB_REQ_PRESS_YN)) as SUB_REQ_PRESS_YN,substring(SUB_REQ_TEMP_YN,0,LEN(SUB_REQ_TEMP_YN)) as SUB_REQ_TEMP_YN
                        ,substring(SUB_REQ_VACC_YN,0,LEN(SUB_REQ_VACC_YN)) as SUB_REQ_VACC_YN,substring(SUB_REQ_START_STOP_YN,0,LEN(SUB_REQ_START_STOP_YN)) as SUB_REQ_START_STOP_YN
                        ,substring(SUB_REQ_WEIGHT_YN,0,LEN(SUB_REQ_WEIGHT_YN)) as SUB_REQ_WEIGHT_YN,substring(SUB_REQ_WEIGHT_SAMPLE_YN,0,LEN(SUB_REQ_WEIGHT_SAMPLE_YN)) as SUB_REQ_WEIGHT_SAMPLE_YN
					
                        from(
                        select BMR_VERSION
                        ,STUFF((select CAST(TP_DESC.BCR_STEP_NO as nvarchar)+'.'+cast(TP_DESC.BCR_RUN_NO as nvarchar)+'|'
							from  BMR_BCR_PROCEDURE TP_DESC 
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_RUN_NO
                        ,STUFF((select TP_DESC.BCR_STEP_DESC+'|'
							from  BMR_BCR_PROCEDURE TP_DESC 
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_DESC
                        ,STUFF((select ISNULL(TP_GROUP_OP.GROUP_NAME+'|','|')
							from  BMR_BCR_PROCEDURE TP_DESC  
							LEFT JOIN  BMR_USER_GROUP TP_GROUP_OP  ON TP_DESC.BCR_GROUP_OPERATE_ID=TP_GROUP_OP.GROUP_ID
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_GROUP_OP
                        ,STUFF((select ISNULL(TP_GROUP_OP.GROUP_NAME+'|','|')
							from  BMR_BCR_PROCEDURE TP_DESC  
							LEFT JOIN  BMR_USER_GROUP TP_GROUP_OP  ON TP_DESC.BCR_GROUP_CHECK_ID=TP_GROUP_OP.GROUP_ID
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_GROUP_CHECK
                        ,STUFF((select ISNULL(TP_DESC.BCR_REQ_IMAGE_YN+'|','N|')
							from  BMR_BCR_PROCEDURE TP_DESC 
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_IMAGE_YN
                        ,STUFF((select ISNULL(TP_DESC.BCR_REQ_HUMIDITY_YN+'|','N|')
							from  BMR_BCR_PROCEDURE TP_DESC 
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_HUMIDITY_YN
                        ,STUFF((select ISNULL(TP_DESC.BCR_REQ_PRESS_YN+'|','N|')
							from  BMR_BCR_PROCEDURE TP_DESC 
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_PRESS_YN
						,STUFF((select ISNULL(TP_DESC.BCR_REQ_TEMP_YN+'|','N|')
							from  BMR_BCR_PROCEDURE TP_DESC 
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_TEMP_YN
						,STUFF((select ISNULL(TP_DESC.BCR_REQ_VACC_YN+'|','N|')
							from  BMR_BCR_PROCEDURE TP_DESC 
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_VACC_YN
						,STUFF((select ISNULL(TP_DESC.BCR_REQ_START_STOP_YN+'|','N|')
							from  BMR_BCR_PROCEDURE TP_DESC 
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_START_STOP_YN
						,STUFF((select ISNULL(TP_DESC.BCR_REQ_WEIGHT_YN+'|','N|')
							from  BMR_BCR_PROCEDURE TP_DESC 
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_WEIGHT_YN
						,STUFF((select ISNULL(TP_DESC.BCR_REQ_WEIGHT_SAMPLE_YN+'|','N|')
							from  BMR_BCR_PROCEDURE TP_DESC 
							where TP_DESC.BCR_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCR_STEP_NO=TP_MAIN.BCR_STEP_NO
							and TP_DESC.BCR_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_WEIGHT_SAMPLE_YN
                        ,BCR_BMR_H_SYS_ID,BCR_STEP_NO,BCR_RUN_NO,BCR_STEP_DESC,GROUP_OP.GROUP_NAME AS GROUP_OP_NAME
                        ,GROUP_CHECK.GROUP_NAME AS GROUP_CHECK_NAME
                        ,BCR_REQ_IMAGE_YN,BCR_REQ_HUMIDITY_YN,BCR_REQ_PRESS_YN,BCR_REQ_TEMP_YN,BCR_REQ_VACC_YN
                        ,BCR_REQ_START_STOP_YN,BCR_REQ_WEIGHT_YN,BCR_REQ_WEIGHT_SAMPLE_YN,BCR_STANDARD_W
                        from BMR_HEAD LEFT JOIN BMR_BCR_PROCEDURE TP_MAIN ON BMR_H_SYS_ID = BCR_BMR_H_SYS_ID
                        LEFT JOIN BMR_USER_GROUP GROUP_OP ON BCR_GROUP_OPERATE_ID=GROUP_OP.GROUP_ID
                        LEFT JOIN BMR_USER_GROUP GROUP_CHECK ON BCR_GROUP_OPERATE_ID=GROUP_CHECK.GROUP_ID
                        WHERE BMR_ITEM_CODE = @P_ITEM_CODE 
                       and BMR_RV1 = @P_RV1
						and BMR_RV2 = @P_RV2
						and CONVERT(VARCHAR,BMR_RV_DATE,4) = @P_RV_DT
                        and BCR_RUN_NO=0)data1
                        ORDER BY BCR_STEP_NO,BCR_RUN_NO";
            return query;
        }
        public String QueryGetBcaCleanControl()
        {
            query = @"select BCA_CC_BMR_H_SYS_ID,BCA_CC_RUN_NO,BCA_CC_EQUIPMENT_NO,BCA_CC_EQUIPMENT_NAME from BMR_HEAD LEFT JOIN BMR_BCA_CLEAN_CONTROL ON BCA_CC_BMR_H_SYS_ID = BMR_H_SYS_ID 
                        WHERE BMR_ITEM_CODE= @P_ITEM_CODE 
                        and BMR_RV1 = @P_RV1
						and BMR_RV2 = @P_RV2
						and CONVERT(VARCHAR,BMR_RV_DATE,4) = @P_RV_DT";
            return query;
        }
        public String QueryGerBca()
        {
            //query = @"select BCA_BMR_H_SYS_ID,BCA_STEP_NO,BCA_RUN_NO,BCA_STEP_DESC from BMR_HEAD LEFT JOIN BMR_BCA_PROCEDURE ON BMR_H_SYS_ID = BCA_BMR_H_SYS_ID
            //          WHERE BMR_ITEM_CODE = @P_ITEM_CODE AND BMR_VERSION = @P_VERSION";
            query = @"select BCA_BMR_H_SYS_ID,BCA_STEP_NO,BCA_STEP_DESC,BMR_VERSION
                        ,substring(SUB_RUN_NO,0,LEN(SUB_RUN_NO)) as SUB_RUN_NO,substring(SUB_DESC,0,LEN(SUB_DESC)) as SUB_DESC
                        ,substring(SUB_GROUP_OP,0,LEN(SUB_GROUP_OP)) as SUB_GROUP_OP,substring(SUB_GROUP_CHECK,0,LEN(SUB_GROUP_CHECK)) as SUB_GROUP_CHECK
                        ,substring(SUB_REQ_IMAGE_YN,0,LEN(SUB_REQ_IMAGE_YN)) as SUB_REQ_IMAGE_YN,substring(SUB_REQ_HUMIDITY_YN,0,LEN(SUB_REQ_HUMIDITY_YN)) as SUB_REQ_HUMIDITY_YN
                        ,substring(SUB_REQ_PRESS_YN,0,LEN(SUB_REQ_PRESS_YN)) as SUB_REQ_PRESS_YN,substring(SUB_REQ_TEMP_YN,0,LEN(SUB_REQ_TEMP_YN)) as SUB_REQ_TEMP_YN
                        ,substring(SUB_REQ_VACC_YN,0,LEN(SUB_REQ_VACC_YN)) as SUB_REQ_VACC_YN,substring(SUB_REQ_START_STOP_YN,0,LEN(SUB_REQ_START_STOP_YN)) as SUB_REQ_START_STOP_YN
                        ,substring(SUB_REQ_WEIGHT_YN,0,LEN(SUB_REQ_WEIGHT_YN)) as SUB_REQ_WEIGHT_YN,substring(SUB_REQ_WEIGHT_SAMPLE_YN,0,LEN(SUB_REQ_WEIGHT_SAMPLE_YN)) as SUB_REQ_WEIGHT_SAMPLE_YN
					
                        from(
                        select BMR_VERSION
                        ,STUFF((select CAST(TP_DESC.BCA_STEP_NO as nvarchar)+'.'+cast(TP_DESC.BCA_RUN_NO as nvarchar)+'|'
							from  BMR_BCA_PROCEDURE TP_DESC 
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_RUN_NO
                        ,STUFF((select TP_DESC.BCA_STEP_DESC+'|'
							from  BMR_BCA_PROCEDURE TP_DESC 
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_DESC
                        ,STUFF((select ISNULL(TP_GROUP_OP.GROUP_NAME+'|','|')
							from  BMR_BCA_PROCEDURE TP_DESC  
							LEFT JOIN  BMR_USER_GROUP TP_GROUP_OP  ON TP_DESC.BCA_GROUP_OPERATE_ID=TP_GROUP_OP.GROUP_ID
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_GROUP_OP
                        ,STUFF((select ISNULL(TP_GROUP_OP.GROUP_NAME+'|','|')
							from  BMR_BCA_PROCEDURE TP_DESC  
							LEFT JOIN  BMR_USER_GROUP TP_GROUP_OP  ON TP_DESC.BCA_GROUP_CHECK_ID=TP_GROUP_OP.GROUP_ID
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_GROUP_CHECK
                        ,STUFF((select ISNULL(TP_DESC.BCA_REQ_IMAGE_YN+'|','N|')
							from  BMR_BCA_PROCEDURE TP_DESC 
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_IMAGE_YN
                        ,STUFF((select ISNULL(TP_DESC.BCA_REQ_HUMIDITY_YN+'|','N|')
							from  BMR_BCA_PROCEDURE TP_DESC 
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_HUMIDITY_YN
                        ,STUFF((select ISNULL(TP_DESC.BCA_REQ_PRESS_YN+'|','N|')
							from  BMR_BCA_PROCEDURE TP_DESC 
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_PRESS_YN
						,STUFF((select ISNULL(TP_DESC.BCA_REQ_TEMP_YN+'|','N|')
							from  BMR_BCA_PROCEDURE TP_DESC 
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_TEMP_YN
						,STUFF((select ISNULL(TP_DESC.BCA_REQ_VACC_YN+'|','N|')
							from  BMR_BCA_PROCEDURE TP_DESC 
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_VACC_YN
						,STUFF((select ISNULL(TP_DESC.BCA_REQ_START_STOP_YN+'|','N|')
							from  BMR_BCA_PROCEDURE TP_DESC 
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_START_STOP_YN
						,STUFF((select ISNULL(TP_DESC.BCA_REQ_WEIGHT_YN+'|','N|')
							from  BMR_BCA_PROCEDURE TP_DESC 
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_WEIGHT_YN
						,STUFF((select ISNULL(TP_DESC.BCA_REQ_WEIGHT_SAMPLE_YN+'|','N|')
							from  BMR_BCA_PROCEDURE TP_DESC 
							where TP_DESC.BCA_BMR_H_SYS_ID=BMR_H_SYS_ID
							and TP_DESC.BCA_STEP_NO=TP_MAIN.BCA_STEP_NO
							and TP_DESC.BCA_RUN_NO>0
                            FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)') 
                        ,1,0,'') SUB_REQ_WEIGHT_SAMPLE_YN
                        ,BCA_BMR_H_SYS_ID,BCA_STEP_NO,BCA_RUN_NO,BCA_STEP_DESC,GROUP_OP.GROUP_NAME AS GROUP_OP_NAME
                        ,GROUP_CHECK.GROUP_NAME AS GROUP_CHECK_NAME
                        ,BCA_REQ_IMAGE_YN,BCA_REQ_HUMIDITY_YN,BCA_REQ_PRESS_YN,BCA_REQ_TEMP_YN,BCA_REQ_VACC_YN
                        ,BCA_REQ_START_STOP_YN,BCA_REQ_WEIGHT_YN,BCA_REQ_WEIGHT_SAMPLE_YN,BCA_STANDARD_W
                        from BMR_HEAD LEFT JOIN BMR_BCA_PROCEDURE TP_MAIN ON BMR_H_SYS_ID = BCA_BMR_H_SYS_ID
                        LEFT JOIN BMR_USER_GROUP GROUP_OP ON BCA_GROUP_OPERATE_ID=GROUP_OP.GROUP_ID
                        LEFT JOIN BMR_USER_GROUP GROUP_CHECK ON BCA_GROUP_OPERATE_ID=GROUP_CHECK.GROUP_ID
                        WHERE BMR_ITEM_CODE = @P_ITEM_CODE 
                        and BMR_RV1 = @P_RV1
						and BMR_RV2 = @P_RV2
						and CONVERT(VARCHAR,BMR_RV_DATE,4) = @P_RV_DT
                        and BCA_RUN_NO=0)data1
                        ORDER BY BCA_STEP_NO,BCA_RUN_NO";
            return query;
        }
        public String QueryGetVersionByItem()
        {
            if (Convert.ToBoolean(HttpContext.Current.Session["AUTH_DISABLE_BMR"]))
            {
                query = @"select BMR_ITEM_CODE,BMR_VERSION,BMR_START_DATE,BMR_END_DATE 
                        ,'R'+CAST(BMR_RV1 AS NVARCHAR)+'-'+CAST((BMR_RV2) AS NVARCHAR)+'/'+CONVERT(varchar,BMR_RV_DATE,4)  AS REVISION
                        from bmr_head where UPPER(BMR_ITEM_CODE) = @P_ITEM_CODE";
            }
            else
            {
                query = @"select BMR_ITEM_CODE,BMR_VERSION,BMR_START_DATE,BMR_END_DATE
                        ,'R'+CAST(BMR_RV1 AS NVARCHAR)+'-'+CAST((BMR_RV2) AS NVARCHAR)+'/'+CONVERT(varchar,BMR_RV_DATE,4)  AS REVISION
                        from bmr_head where UPPER(BMR_ITEM_CODE) = @P_ITEM_CODE 
						and BMR_START_DATE <=SYSDATETIME()
						and CONVERT(date,ISNULL(BMR_END_DATE,SYSDATETIME()),103) >= CONVERT(date,SYSDATETIME(),103)
                        and BMR_STATUS = 1";
            }
            return query;
        }
        public String QueryGetRevivsion() {
            query = @"select CASE WHEN BMR_RV2<=5 THEN 'R'+
                        CAST(BMR_RV1 AS NVARCHAR)+'-'+
                        CAST((BMR_RV2+1) AS NVARCHAR) ELSE 'R'+CAST((BMR_RV1+1) AS NVARCHAR) END+'/'+
                        BMR_RV_DATE AS REVISION
                        from(
                             select BMR_ITEM_CODE
                             ,CAST(MAX(ISNULL(BMR_RV1,0)) AS NVARCHAR) BMR_RV1
                             ,CAST(MAX(ISNULL(BMR_RV2,0)) AS NVARCHAR) BMR_RV2
	                         ,CONVERT(varchar,MAX(BMR_RV_DATE),4) BMR_RV_DATE
                             from BMR_HEAD group by BMR_ITEM_CODE
                        )data1
                        where BMR_ITEM_CODE = @P_ITEM_CODE";
            return query;
        }
    }
}