using System;
using System.Drawing;
using System.Windows.Forms;

namespace ����������Ϸ
{
	
	//*****�Զ�����*****
	//*****��¼����λ���ص���*****
	//�ص�:�Էǳ����ɵ����ݽṹ,��¼��ÿһʱ�������ϵ��������
	//����ʱ��:3��  �Ѷ�:3��
	
	public class FlagContainer
	{
		public bool[] have=new bool[90];   //�����ϸ�����������
		public string[] who=new string[90];//�Ǻ췽��������
		public string[] str=new string[90];//��ʲô����(����)   
		
        public Point[] allPoint=new Point[90];  //��¼���������е������ 
		public Point[] partPoint=new Point[32];  //��ų��տ�ʼʢ��32(��������:�췽/������16��)�����ӵĵ�
		public int[] whichPicture=new int[90];   //��־�����ϵĵ��ϵ�ǰ���ĸ�pictureBox�ؼ�
		
		public int rednum=0;
		public int bluenum=0;
		public int index;

		public int width,height;
		public int first_X,first_Y;

		public int redNumTotal=0;
		public int blueNumTotal=0;
		
		public FlagContainer()
		{	
			Initialize();
		}		
		public FlagContainer(string[] soldiers,Point[] allPoint,Point[] partPoint,int width,int height)		
		{		   
			Initialize(soldiers,allPoint,partPoint,width,height);
		}
		public void Initialize()
		{
			
			//��ʼ��
			for(int i=0;i<90;i++)
				this.who[i]="";			
			for(int i=0;i<90;i++)
				this.str[i]="";			
			for(int i=0;i<90;i++)
				this.have[i]=false;			
			for(int i=0;i<90;i++)
				this.whichPicture[i]=-1;
			
			rednum=0;
			bluenum=0;
			
			for(int i=0;i<90;i++)				
				this.allPoint[i]=new Point();
			for(int i=0;i<32;i++)
				this.partPoint[i]=new Point();			
					
			index=0;
		}
		public void Initialize(string[] soldiers,Point[] allPoint,Point[] partPoint,int width,int height)
		{			
			this.Initialize();

			this.width=width;
			this.height=height;

			first_X=allPoint[0].X;
			first_Y=allPoint[0].Y;

			for(int i=0;i<90;i++)
			{
				this.allPoint[i].X=allPoint[i].X;
				this.allPoint[i].Y=allPoint[i].Y;
			}	
			for(int i=0;i<32;i++)
			{
				this.partPoint[i].X=partPoint[i].X;
				this.partPoint[i].Y=partPoint[i].Y;
			}			

			for(int i=0;i<32;i++)
			{				
				index=this.CountIndex(partPoint[i]);				
				if(soldiers[i]!="NULL")
				{
					if(i<=15)
					{
						have[index]=true;
						who[index]="red";
						str[index]=soldiers[i];
						whichPicture[index]=i;
						rednum++;
					}
					else
					{
						have[index]=true;
						who[index]="blue";
						str[index]=soldiers[i];
						whichPicture[index]=i;
						bluenum++;
					}
				} 				
			} 
			this.blueNumTotal=this.bluenum;
			this.redNumTotal=this.rednum;
		}
		
		public void Initialize(FlagContainer C)
		{
			for(int i=0;i<90;i++)
			{
				this.have[i]=C.have[i];
				this.who[i]=C.who[i];
				this.str[i]=C.str[i];
				this.allPoint[i]=C.allPoint[i];
				if(i<32)
					this.partPoint[i]=C.partPoint[i];
				this.whichPicture[i]=C.whichPicture[i];
				this.rednum=C.rednum;
				this.bluenum=C.bluenum;
				this.index=C.index;
				this.width=C.width;
				this.height=C.height;
				this.first_X=C.first_X;
				this.first_Y=C.first_Y;
			}

		}
		public void InitializePart(Point[] allPoint,int width,int height)
		{
			this.width=width;
			this.height=height;
			for(int i=0;i<90;i++)
			{
				this.allPoint[i].X=allPoint[i].X;
				this.allPoint[i].Y=allPoint[i].Y;
			}			
		}
		public Point GetNearPoint(int x,int y)
		{			
			int nearIndex=0;
			double distance=double.MaxValue;
			for(int i=0;i<90;i++)
			{   //                    ______________________
				//�������ľ��빫ʽ N (x2-x1)^2 - (y2-y2)^2  �ļ�    
				double end=Math.Sqrt((x-allPoint[i].X)*(x-allPoint[i].X)+(y-allPoint[i].Y)*(y-allPoint[i].Y));
				if(end<distance)
				{
					nearIndex=i;
					distance=end;
				}				
			}			
			if(distance<this.height/3+8)
				return allPoint[nearIndex];
			else return new Point(-1,-1);
		}
		public double GetTwoPointDistance(Point p1,Point p2)
		{
			return Math.Sqrt((p1.X-p2.X)*(p1.X-p2.X)+(p1.Y-p2.Y)*(p1.Y-p2.Y));
		}		
		public int CountIndex(Point point)  //width=65,height=46; firstPoint(43,22)
		{
			//����:��֪һ����,����˵�ӳ�䵽һά����(���������ϵ�λ��)��Ӧ������
			//��ʽ:һά�����±�=(y2-y1)/this.height*9+(x2-x1)/this.width		
			if((point.Y-first_Y)%height!=0)
				point.Y-=(height/2);
			int index=(point.Y-first_Y)/height*9+(point.X-first_X)/width;
			return index;
		}
		public int CountIndex(int x,int y)
		{
			if((y-first_Y)%height!=0)
				y-=(height/2);
			int index=(y-first_Y)/height*9+(x-first_X)/width;
			return index;
		}
		public int GetIndexByPictureIndex(int pictureBoxIndex)
		{
			int i;
			for(i=0;i<90;i++)
				if(this.whichPicture[i]==pictureBoxIndex)
					break;
			if(i==90)
				return -1;
			return i;
		}
		public Point CountPoint(int index)
		{
			//��֪һά�����һ������,������Ӧ�ĵ�
			if(index>=0&&index<90)
			{
				for(int i=0;i<90;i++)
					if(index==this.CountIndex(this.allPoint[i]))
						return allPoint[i];
			}
			else
				return new Point(-1,-1);
			return new Point(-1,-1);
		}
		public string IsUpOrDown(Point point)
		{
			int index=this.CountIndex(point);
			if(index>=0&&index<45)
				return "up";
			else 
			{
				if(index>=45&&index<90)
					return "down";
			}
			MessageBox.Show("IsUpOrDown����");
			return "wrong";
		}

	}
}