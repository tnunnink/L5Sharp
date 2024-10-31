namespace L5Sharp.Samples;

public static class Sample
{
    public static class TagElement
    {
        public static string TestComplexTag()
        {
            return
                @"<Tag Name=""TestComplexTag"" TagType=""Base"" DataType=""ComplexType"" Constant=""false"" ExternalAccess=""None"">
                <Description>
                    <![CDATA[Base]]>
                </Description>
                <Data Format=""L5K"">
                    <![CDATA[[[0,0,0,0,0,0.00000000e+000],[0,0,0],[0,0,0],[1,0.00000000e+000,3.40282347e+038,3.40282347e+038,-3.40282347e+038
		,-3.40282347e+038,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0,0.00000000e+000
		,0,5.60519386e-045,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000
		,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000],[1,0,0,0,[0,0
		,0,0,0,0.00000000e+000],[0,0,0,0,0],[0,'$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00'
		]],[[0,0,0,0,0,0.00000000e+000],[0,0,0,0,0,0.00000000e+000],[0,0,0,0,0,0.00000000e+000],[0,0,0,0,0,0.00000000e+000],[0
		,0,0,0,0,0.00000000e+000]],0,[0,'$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00'
		],[0,'$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00'
		]]]]>
                </Data>
                <Data Format=""Decorated"">
                    <Structure DataType=""ComplexType"">
                        <StructureMember Name=""SimpleMember"" DataType=""SimpleType"">
                            <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                            <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                            <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$00'""/>
                            <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                            <DataValueMember Name=""RealMember"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                        </StructureMember>
                        <StructureMember Name=""CounterMember"" DataType=""COUNTER"">
                            <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                            <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                            <DataValueMember Name=""CU"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""CD"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""OV"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""UN"" DataType=""BOOL"" Value=""0""/>
                        </StructureMember>
                        <StructureMember Name=""TimeMember"" DataType=""TIMER"">
                            <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                            <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                            <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
                        </StructureMember>
                        <StructureMember Name=""AlarmMember"" DataType=""ALARM"">
                            <DataValueMember Name=""EnableIn"" DataType=""BOOL"" Value=""1""/>
                            <DataValueMember Name=""In"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                            <DataValueMember Name=""HHLimit"" DataType=""REAL"" Radix=""Float"" Value=""3.40282347e+038""/>
                            <DataValueMember Name=""HLimit"" DataType=""REAL"" Radix=""Float"" Value=""3.40282347e+038""/>
                            <DataValueMember Name=""LLimit"" DataType=""REAL"" Radix=""Float"" Value=""-3.40282347e+038""/>
                            <DataValueMember Name=""LLLimit"" DataType=""REAL"" Radix=""Float"" Value=""-3.40282347e+038""/>
                            <DataValueMember Name=""Deadband"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                            <DataValueMember Name=""ROCPosLimit"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                            <DataValueMember Name=""ROCNegLimit"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                            <DataValueMember Name=""ROCPeriod"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                            <DataValueMember Name=""EnableOut"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""HHAlarm"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""HAlarm"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""LAlarm"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""LLAlarm"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""ROCPosAlarm"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""ROCNegAlarm"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""ROC"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                            <DataValueMember Name=""Status"" DataType=""DINT"" Radix=""Hex"" Value=""16#0000_0000""/>
                            <DataValueMember Name=""InstructFault"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""DeadbandInv"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""ROCPosLimitInv"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""ROCNegLimitInv"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""ROCPeriodInv"" DataType=""BOOL"" Value=""0""/>
                        </StructureMember>
                        <StructureMember Name=""AOIType"" DataType=""aoi_Test"">
                            <DataValueMember Name=""EnableIn"" DataType=""BOOL"" Value=""1""/>
                            <DataValueMember Name=""EnableOut"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""InputTest"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""OutputTest"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                            <DataValueMember Name=""Config"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                            <DataValueMember Name=""Test"" DataType=""BOOL"" Value=""0""/>
                            <DataValueMember Name=""New"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                            <DataValueMember Name=""Alias"" DataType=""BOOL"" Value=""0""/>
                        </StructureMember>
                        <ArrayMember Name=""SimplArray"" DataType=""SimpleType"" Dimensions=""5"">
                            <Element Index=""[0]"">
                                <Structure DataType=""SimpleType"">
                                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII""
                                                     Value=""'$00$00$00$00'""/>
                                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                                    <DataValueMember Name=""RealMember"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                                </Structure>
                            </Element>
                            <Element Index=""[1]"">
                                <Structure DataType=""SimpleType"">
                                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII""
                                                     Value=""'$00$00$00$00'""/>
                                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                                    <DataValueMember Name=""RealMember"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                                </Structure>
                            </Element>
                            <Element Index=""[2]"">
                                <Structure DataType=""SimpleType"">
                                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII""
                                                     Value=""'$00$00$00$00'""/>
                                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                                    <DataValueMember Name=""RealMember"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                                </Structure>
                            </Element>
                            <Element Index=""[3]"">
                                <Structure DataType=""SimpleType"">
                                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII""
                                                     Value=""'$00$00$00$00'""/>
                                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                                    <DataValueMember Name=""RealMember"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                                </Structure>
                            </Element>
                            <Element Index=""[4]"">
                                <Structure DataType=""SimpleType"">
                                    <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                                    <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                                    <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII""
                                                     Value=""'$00$00$00$00'""/>
                                    <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                                    <DataValueMember Name=""RealMember"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                                </Structure>
                            </Element>
                        </ArrayMember>
                        <DataValueMember Name=""NewMember"" DataType=""BOOL"" Value=""0""/>
                        <StructureMember Name=""StringMember"" DataType=""STRING"">
                            <DataValueMember Name=""LEN"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                            <DataValueMember Name=""DATA"" DataType=""STRING"" Radix=""ASCII"">
                                <![CDATA[]]>
                            </DataValueMember>
                        </StructureMember>
                        <StructureMember Name=""MyStringMember"" DataType=""MyStringType"">
                            <DataValueMember Name=""LEN"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                            <DataValueMember Name=""DATA"" DataType=""MyStringType"" Radix=""ASCII"">
                                <![CDATA[]]>
                            </DataValueMember>
                        </StructureMember>
                    </Structure>
                </Data>
            </Tag>";
        }

        public static string TestSimpleTag()
        {
            return
                @"<Tag Name=""TestSimpleTag"" TagType=""Base"" DataType=""SimpleType"" Constant=""false"" ExternalAccess=""Read Only"">
            <Data Format=""L5K"">
            <![CDATA[[0,0,14,1,0,0.00000000e+000]]]>
            </Data>
            <Data Format=""Decorated"">
            <Structure DataType=""SimpleType"">
            <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
            <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
            <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_016""/>
            <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$01'""/>
            <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
            <DataValueMember Name=""RealMember"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
            </Structure>
            </Data>
            </Tag>";
        }

        public static string TestStringTag()
        {
            return
                @"<Tag Name=""TestStringTag"" TagType=""Base"" DataType=""MyStringType"" Constant=""false"" ExternalAccess=""None"">
            <Description>
            <![CDATA[String]]>
        </Description>
            <Comments>
            <Comment Operand="".DATA"">
            <![CDATA[Data]]>
        </Comment>
            </Comments>
            <Data Format=""L5K"">
            <![CDATA[[17,'This is a $$ tests$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00$00'
            ]]]>
            </Data>
            <Data Format=""String"" Length=""17"">
            <![CDATA['This is a $$ tests']]>
            </Data>
            </Tag>";
        }

        public static string TestTimerTag()
        {
            return
                @"<Tag Name=""TestTimer"" TagType=""Base"" DataType=""TIMER"" Constant=""false"" ExternalAccess=""None"">
            <Comments>
            <Comment Operand="".PRE"">
            <![CDATA[Test Timer PRE]]>
            </Comment>
            </Comments>
            <Data Format=""L5K"">
            <![CDATA[[0,1000,0]]]>
            </Data>
            <Data Format=""Decorated"">
            <Structure DataType=""TIMER"">
            <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""1000""/>
            <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
            <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0""/>
            <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0""/>
            <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
            </Structure>
            </Data>
            </Tag>";
        }

        public static string TestSimpleBool()
        {
            return @"<Tag Name=""SimpleBool"" TagType=""Base"" DataType=""BOOL"" Radix=""Decimal"" Constant=""false""
                 ExternalAccess=""Read/Write"">
                <Data Format=""L5K"">
                    <![CDATA[0]]>
                </Data>
                <Data Format=""Decorated"">
                    <DataValue DataType=""BOOL"" Radix=""Decimal"" Value=""0""/>
                </Data>
            </Tag>";
        }

        public static string TestSimpleDint()
        {
            return @"<Tag Name=""SimpleDint"" TagType=""Base"" DataType=""DINT"" Radix=""Decimal"" Constant=""false""
                 ExternalAccess=""None"">
                <Data Format=""L5K"">
                    <![CDATA[123392]]>
                </Data>
                <Data Format=""Decorated"">
                    <DataValue DataType=""DINT"" Radix=""Decimal"" Value=""123392""/>
                </Data>
            </Tag>";
        }

        public static string TestSimpleInt()
        {
            return
                @"<Tag Name=""SimpleInt"" TagType=""Base"" DataType=""INT"" Radix=""Decimal"" Constant=""false"" ExternalAccess=""None"">
            <Description>
            <![CDATA[This is a simple integer tag]]>
            </Description>
            <Data Format=""L5K"">
            <![CDATA[4321]]>
            </Data>
            <Data Format=""Decorated"">
            <DataValue DataType=""INT"" Radix=""Decimal"" Value=""4321""/>
            </Data>
            </Tag>";
        }

        public static string TestSimpleLint()
        {
            return @"<Tag Name=""SimpleLint"" TagType=""Base"" DataType=""LINT"" Radix=""Decimal"" Constant=""false""
        ExternalAccess=""None"">
            <Data Format=""L5K"">
            <![CDATA[123]]>
            </Data>
            <Data Format=""Decorated"">
            <DataValue DataType=""LINT"" Radix=""Decimal"" Value=""123""/>
            </Data>
            </Tag>";
        }

        public static string TestSimpleReal()
        {
            return
                @"<Tag Name=""SimpleReal"" TagType=""Base"" DataType=""REAL"" Radix=""Float"" Constant=""false"" ExternalAccess=""None"">
            <Data Format=""L5K"">
            <![CDATA[1.23000000e+000]]>
            </Data>
            <Data Format=""Decorated"">
            <DataValue DataType=""REAL"" Radix=""Float"" Value=""1.23""/>
            </Data>
            </Tag>";
        }

        public static string TestSimpleSint()
        {
            return
                @"<Tag Name=""SimpleSint"" TagType=""Base"" DataType=""SINT"" Radix=""Hex"" Constant=""false"" ExternalAccess=""None"">
            <Comments>
            <Comment Operand="".0"">
            <![CDATA[This is a test]]>
            </Comment>
            </Comments>
            <Data Format=""L5K"">
            <![CDATA[12]]>
            </Data>
            <Data Format=""Decorated"">
            <DataValue DataType=""SINT"" Radix=""Hex"" Value=""16#0c""/>
            </Data>
            </Tag>";
        }
    }

    public static class DataTypeElement
    {
        public static string SimpleType()
        {
            return @"<DataType Name=""SimpleType"" Family=""NoFamily"" Class=""User"">
                <Description>
                    <![CDATA[This is a test data type that contains simple atomic types with an updated description]]>
                </Description>
                <Members>
                    <Member Name=""ZZZZZZZZZZSimpleType0"" DataType=""SINT"" Dimension=""0"" Radix=""Decimal"" Hidden=""true""
                            ExternalAccess=""Read/Write""/>
                    <Member Name=""BoolMember"" DataType=""BIT"" Dimension=""0"" Radix=""Hex"" Hidden=""false""
                            Target=""ZZZZZZZZZZSimpleType0"" BitNumber=""0"" ExternalAccess=""Read/Write"">
                        <Description>
                            <![CDATA[Test Bool]]>
                        </Description>
                    </Member>
                    <Member Name=""SintMember"" DataType=""SINT"" Dimension=""0"" Radix=""Hex"" Hidden=""false""
                            ExternalAccess=""Read/Write"">
                        <Description>
                            <![CDATA[Test Sint]]>
                        </Description>
                    </Member>
                    <Member Name=""IntMember"" DataType=""INT"" Dimension=""0"" Radix=""Octal"" Hidden=""false""
                            ExternalAccess=""Read/Write"">
                        <Description>
                            <![CDATA[Test Int]]>
                        </Description>
                    </Member>
                    <Member Name=""DintMember"" DataType=""DINT"" Dimension=""0"" Radix=""ASCII"" Hidden=""false""
                            ExternalAccess=""None"">
                        <Description>
                            <![CDATA[Test Dint]]>
                        </Description>
                    </Member>
                    <Member Name=""LintMember"" DataType=""LINT"" Dimension=""0"" Radix=""Decimal"" Hidden=""false""
                            ExternalAccess=""Read/Write"">
                        <Description>
                            <![CDATA[Test Lint]]>
                        </Description>
                    </Member>
                    <Member Name=""RealMember"" DataType=""REAL"" Dimension=""0"" Radix=""Float"" Hidden=""false""
                            ExternalAccess=""Read/Write"">
                        <Description>
                            <![CDATA[Test Real]]>
                        </Description>
                    </Member>
                </Members>
            </DataType>";
        }
    }

    public static class RungElement
    {
        public static string MainProgramRung0()
        {
            return @" <Rung Number=""0"" Type=""N"">
                        <Text>
                            <![CDATA[TON(TestTimer,?,?);]]>
                        </Text>
                    </Rung>";
        }

        public static string MainProgramRung1()
        {
            return @"<Rung Number=""1"" Type=""N"">
                        <Text>
                            <![CDATA[MOV(16#20,SimpleSint);]]>
                        </Text>
                    </Rung>";
        }
        
        public static string MainProgramRung2()
        {
            return @"<Rung Number=""2"" Type=""N"">
                        <Text>
                            <![CDATA[aoi_Test(aoiTestInstance,TestSimpleTag,SimpleInt,RealArray,0);]]>
                        </Text>
                    </Rung>";
        }
        
        public static string MainProgramRung3()
        {
            return @"<Rung Number=""3"" Type=""N"">
                        <Text>
                            <![CDATA[[XIC(SimpleBool) ,XIC(SimpleBool) ][OTE(SimpleBool) ,OTU(SimpleBool) ];]]>
                        </Text>
                    </Rung>";
        }
        
        public static string MainProgramRung4()
        {
            return @"<Rung Number=""4"" Type=""N"">
                        <Text>
                            <![CDATA[OTE(TestComplexTag.SimpleMember.BoolMember);]]>
                        </Text>
                    </Rung>";
        }
        
        public static string MainProgramRung5()
        {
            return @"<Rung Number=""5"" Type=""N"">
                        <Text>
                            <![CDATA[MOV(SimpleSint,AsciiTag);]]>
                        </Text>
                    </Rung>";
        }
        
        public static string MainProgramRung6()
        {
            return @"<Rung Number=""6"" Type=""N"">
                        <Text>
                            <![CDATA[XIC(FlexIO:3:I.Pt01.Data)OTE(BufferTag);]]>
                        </Text>
                    </Rung>";
        }
        
        public static string MainProgramRung7()
        {
            return @"<Rung Number=""7"" Type=""N"">
                        <Text>
                            <![CDATA[JSR(FBD,1,InputParameter,OutputParameter);]]>
                        </Text>
                    </Rung>";
        }
        
        public static string MainProgramRung8()
        {
            return @"<Rung Number=""8"" Type=""N"">
                        <Text>
                            <![CDATA[GRT(SimpleInt,100)OTE(SimpleArray[4].0);]]>
                        </Text>
                    </Rung>";
        }
        
        public static string MainProgramRung9()
        {
            return @"<Rung Number=""9"" Type=""N"">
                        <Text>
                            <![CDATA[GRT(SimpleInt,400)XIO(MultiDimensionalArray[1,3].3)CMP(ATN(_Test) > 1.0)[TON(TimerArray[0],?,?) ,OTU(TestComplexTag.SimpleMember.BoolMember) ];]]>
                        </Text>
                    </Rung>";
        }
        
        public static string MainProgramRung10()
        {
            return @"<Rung Number=""10"" Type=""N"">
                        <Text>
                            <![CDATA[CPT(Computation,I01/(TestSimpleTag.DintMember * 100));]]>
                        </Text>
                    </Rung>";
        }
        
        public static string MainProgramRung11()
        {
            return @"<Rung Number=""11"" Type=""N"">
                        <Text>
                            <![CDATA[JSR(FBD,2,AlarmActive,AliasTag,SimpleBool,Another,aoi_Test_001);]]>
                        </Text>
                    </Rung>";
        }
        
        public static string MainProgramRung12()
        {
            return @"<Rung Number=""12"" Type=""N"">
                        <Text>
                            <![CDATA[MOV(FlexIO:1:C.Ch00.HHAlarmLimit,AlarmLimit);]]>
                        </Text>
                    </Rung>";
        }
    }
}