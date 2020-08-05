//接受一個有向圖的權重矩陣，和一個起點編號start（從0編號，頂點存在數組中)
	//返回一個int[] 數組，表示從start到它的最短路徑長度  
	public static int[] Dijsktra(int[][]weight,int start){
		int length = weight.length;
		int[] shortPath = new int[length];//存放從start到各個點的最短距離
		shortPath[0] = 0;//start到他本身的距離最短為0
		String path[] = new String[length];//存放從start點到各點的最短路徑的字符串表示
		for(int i=0;i<length;i++){
			path[i] = start+"->"+i;
		}
		int visited[] = new int[length];//標記當前該頂點的最短路徑是否已經求出，1表示已經求出
		visited[0] = 1;//start點的最短距離已經求出
		for(int count = 1;count<length;count++){
			int k=-1;
			int dmin = Integer.MAX_VALUE;
			for(int i=0;i<length;i++){
				if(visited[i]==0 && weight[start][i]<dmin){
					dmin = weight[start][i];
					k=i;
				}
			}
			//選出一個距離start最近的未標記的頂點     將新選出的頂點標記為以求出最短路徑，且到start的最短路徑為dmin。
			shortPath[k] = dmin;
			visited[k] = 1;
			//以k為中間點，修正從start到未訪問各點的距離
			for(int i=0;i<length;i++){
				if(visited[i]==0 && weight[start][k]+weight[k][i]<weight[start][i]){
					weight[start][i] = weight[start][k]+weight[k][i];
					path[i] = path[k]+"->"+i;
				}
			}
		}
		for(int i=0;i<length;i++){
			System.out.println("從"+start+"出發到"+i+"的最短路徑為："+path[i]+"="+shortPath[i]);
		}
		return shortPath;
		
	}